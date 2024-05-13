using Application.Common.Interfaces;
using Application.TelegramBot.Auth.Models;
using Application.TelegramBot.TelegramUsers.Commands;
using Application.TelegramBot.TelegramUsers.Queries;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.Auth.Commands;

public sealed class AuthenticateTelegramBotCommand : IRequest<TelegramAuthResult>
{
    public string UserMessage { get; init; }
    public long ChatId { get; init; } = default!;
    public long UserId { get; init; } = default!;
    public string UserName { get; init; }
    public string UserFirstName { get; init; }
    public string UserLastName { get; init; }
    public string LanguageCode { get; init; }
}

public sealed class AuthenticateTelegramBotCommandValidator : AbstractValidator<AuthenticateTelegramBotCommand>
{
    public AuthenticateTelegramBotCommandValidator()
    {
        RuleFor(cmd => cmd.ChatId).NotEmpty();
        RuleFor(cmd => cmd.UserId).NotEmpty();
    }
}

public sealed class AuthenticateTelegramBotCommandHandler : IRequestHandler<AuthenticateTelegramBotCommand, TelegramAuthResult>
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserInitializer _currentUserInitializer;

    public AuthenticateTelegramBotCommandHandler(
        IMediator mediator,
        ICurrentUserInitializer currentUserInitializer)
    {
        _mediator = mediator;
        _currentUserInitializer = currentUserInitializer;
    }

    public async Task<TelegramAuthResult> Handle(AuthenticateTelegramBotCommand command, CancellationToken token)
    {
        var telegramUser = await _mediator.Send(new GetTelegramUserQuery(command.UserId));
        if (telegramUser == null)
        {
            if (command.UserMessage == null)
            {
                return TelegramAuthResult.UserNotFound;
            }

            var authCodeValidationResult = await _mediator.Send(new ValidateTelegramAuthCodeCommand(command.UserMessage!));
            if (!authCodeValidationResult.Succeeded)
            {
                return TelegramAuthResult.CodeNotValid;
            }

            telegramUser = await _mediator.Send(new CreateTelegramUserCommand
            {
                UserId = authCodeValidationResult.CurrentUser.UserId,
                OriginalTelegramUserId = command.UserId,
                TelegramChatId = command.ChatId,
                TelegramUserName = command.UserName,
                TelegramUserFirstName = command.UserFirstName,
                TelegramUserLastName = command.UserLastName,
                LanguageCode = command.LanguageCode
            });

            await _mediator.Send(new DeleteTelegramAuthCodeCommand
            {
                UserId = telegramUser.UserId
            });

            return TelegramAuthResult.FirstLogin;
        }

        await _currentUserInitializer.InitializeAsync(telegramUser.UserId);
        return TelegramAuthResult.Succeeded;
    }
}