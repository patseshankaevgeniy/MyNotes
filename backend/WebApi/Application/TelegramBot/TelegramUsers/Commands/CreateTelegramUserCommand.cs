using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TelegramBot.TelegramUser.Models;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.TelegramUsers.Commands;

public sealed class CreateTelegramUserCommand : IRequest<TelegramUserModel>
{
    public Guid UserId { get; init; }
    public long OriginalTelegramUserId { get; init; }
    public long TelegramChatId { get; init; }
    public string TelegramUserName { get; init; }
    public string TelegramUserFirstName { get; init; }
    public string TelegramUserLastName { get; init; }
    public string LanguageCode { get; init; }
}

public sealed class CreateTelegramUserCommandValidator : AbstractValidator<CreateTelegramUserCommand>
{
    public CreateTelegramUserCommandValidator()
    {
        RuleFor(t => t.UserId).NotEmpty();
        RuleFor(t => t.OriginalTelegramUserId).NotEmpty();
        RuleFor(t => t.TelegramChatId).NotEmpty();
    }
}

public sealed class CreateTelegramUserCommandHandler : IRequestHandler<CreateTelegramUserCommand, TelegramUserModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;
    private readonly IGuidService _guidService;
    private readonly IConfiguration _configuration;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMediator _mediator;
    private readonly IMessageService _messageService;

    public CreateTelegramUserCommandHandler(
        IMapper mapper,
        IApplicationDbContext db,
        IGuidService guidService,
        IConfiguration configuration,
        IDateTimeService dateTimeService,
        IMediator mediator,
        IMessageService messageService)
    {
        _db = db;
        _mapper = mapper;
        _guidService = guidService;
        _configuration = configuration;
        _dateTimeService = dateTimeService;
        _mediator = mediator;
        _messageService = messageService;
    }

    public async Task<TelegramUserModel> Handle(CreateTelegramUserCommand command, CancellationToken token)
    {
        var user = await _db.Users.FirstOrDefaultAsync(t => t.Id == command.UserId, token);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), command.UserId);
        }

        var telegramUser = new Domain.Entities.NewFolder.TelegramUser
        {
            Id = _guidService.NewGuid(),
            UserId = user.Id,
            OriginalTelegramUserId = command.OriginalTelegramUserId,
            TelegramChatId = command.TelegramChatId,
            TelegramUserName = command.TelegramUserName,
            TelegramUserFirstName = command.TelegramUserFirstName,
            TelegramUserLastName = command.TelegramUserLastName,
            LanguageCode = command.LanguageCode,
            TelegramBotToken = _configuration["TelegramToken"],
            CreatedDate = _dateTimeService.Now
        };

        _db.TelegramUsers.Add(telegramUser);
        await _db.SaveChangesAsync(token);

        await _messageService.SendTelegramUserCreatedAsync(telegramUser.Id, user.Id);

        return _mapper.Map<TelegramUserModel>(telegramUser);
    }
}