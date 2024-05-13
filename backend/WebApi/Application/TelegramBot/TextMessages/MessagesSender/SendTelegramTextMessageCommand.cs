using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TelegramBot.Common.Models;
using Application.TelegramBot.Common.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.TextMessages.MessagesSender;

public sealed class SendTelegramTextMessageCommand : IRequest<BotMessage>
{
    public Guid? SessionId { get; init; }
    public Guid? UserId { get; init; }
    public string Text { get; init; }
    public IEnumerable<MessageButtonModel>? Buttons { get; init; }
}

public sealed class SendTelegramTextMessageCommandValidator : AbstractValidator<SendTelegramTextMessageCommand>
{
    public SendTelegramTextMessageCommandValidator()
    {
        RuleFor(cmd => cmd.Text).NotEmpty();
    }
}

public sealed class SendTelegramTextMessageCommandHandler : IRequestHandler<SendTelegramTextMessageCommand, BotMessage>
{
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITelegramMessageSender _telegramMessageSender;

    public SendTelegramTextMessageCommandHandler(
        IMediator mediator,
        IApplicationDbContext db,
        ICurrentUserService currentUserService,
        ITelegramMessageSender telegramMessageSender)
    {
        _mediator = mediator;
        _db = db;
        _currentUserService = currentUserService;
        _telegramMessageSender = telegramMessageSender;
    }

    public async Task<BotMessage> Handle(SendTelegramTextMessageCommand command, CancellationToken token)
    {
        var telegramUser = await _db.TelegramUsers
            .AsNoTracking()
            .Where(t => t.UserId == (command.UserId ?? _currentUserService.UserId))
            .FirstOrDefaultAsync(token);

        if (telegramUser == null)
        {
            throw new NotFoundException(nameof(TelegramUser), command.UserId ?? _currentUserService.UserId);
        }

        var botMessage = await _telegramMessageSender.SendTextMessageAsync(telegramUser.TelegramChatId, command.Text!, command.Buttons);

        //if (command.SessionId.HasValue)
        //{
        //    await _mediator.Send(new CreateTelegramSessionMessageCommand
        //    {
        //        MessageId = botMessage.Id,
        //        UserId = telegramUser.UserId,
        //        SessionId = command.SessionId.Value,
        //        State = TelegramMessageState.Sent,
        //        Type = command.Buttons == null
        //            ? TelegramSessionMessageType.TextMessageToUser
        //            : TelegramSessionMessageType.TextMessageWithCallbackToUser
        //    });
        //}

        return botMessage;
    }
}