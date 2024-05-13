using Application.Common.Interfaces;
using Application.TelegramBot.Common.Models;
using Application.TelegramBot.Common.Services.Interfaces;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Application.UserNotes.Commands;
using Domain.Entities;
using Application.TelegramBot.TextMessages.MessagesSender;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.TelegramBot.UserNotes;

public sealed class AddUserNoteBotCommand : IRequest<BotCommandResult>
{
    public string Text { get; set; }
    //public int DaysOfActual { get; init; }
}

public sealed class AddUserNoteBotCommandValidator : AbstractValidator<AddUserNoteBotCommand>
{
    public AddUserNoteBotCommandValidator()
    {
        RuleFor(x => x.Text).NotEmpty();
        // RuleFor(x => x.DaysOfActual).NotEmpty();
    }
}

public sealed class AddPurchaseBotCommandHandler : IRequestHandler<AddUserNoteBotCommand, BotCommandResult>
{
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITelegramBotMessagesProvider _messagesProvider;

    public AddPurchaseBotCommandHandler(
        IMediator mediator,
        IApplicationDbContext db,
        ICurrentUserService currentUserService,
        ITelegramBotMessagesProvider telegramBotMessagesProvider)
    {
        _mediator = mediator;
        _db = db;
        _currentUserService = currentUserService;
        _messagesProvider = telegramBotMessagesProvider;
    }

    public async Task<BotCommandResult> Handle(AddUserNoteBotCommand command, CancellationToken token)
    {
        var createUserNoteCommand = new CreateUserNoteCommand
        {
            Text = command.Text!,
            Source = Source.TelegramBot
        };
        var userNoteModel = await _mediator.Send(createUserNoteCommand, token);

        var botMessage = await _mediator.Send(
           new SendTelegramTextMessageCommand
           {
               Text = _messagesProvider.Get(t => t.UserNoteAdded)
           }, token);

        await Task.Delay(3_000, token);

        if (_currentUserService.UserHasMembers)
        {
            foreach (var userId in
                     _currentUserService.UserGroupMembersIds.Where(id => id != _currentUserService.UserId))
            {
                var telegramUser = _db.TelegramUsers
                    .FirstOrDefault(tUser => tUser.UserId == userId);


                await _mediator.Send(
                    new SendTelegramTextMessageCommand
                    {
                        UserId = userId,
                        Text = string.Format(
                            _messagesProvider.Get(t => t.MemberUserNoteAdded),
                            _currentUserService.User.FirstName,
                            userNoteModel.Text,
                            userNoteModel.Priority)
                    }, token);
            }
        }

        return new() { Succeeded = true };
    }
}



