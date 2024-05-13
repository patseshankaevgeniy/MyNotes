using Application.Auth.Models;
using Application.Common.Interfaces;
using Application.TelegramBot.TelegramUsers.Queries;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands;

public sealed class LoginWithTelegramUserCommand : IRequest<LogInResultModel>
{
    public long TelegramUserId { get; init; } = default!;
}

public sealed class LoginWithTelegramUserCommandValidator : AbstractValidator<LoginWithTelegramUserCommand>
{
    public LoginWithTelegramUserCommandValidator()
    {
        RuleFor(t => t.TelegramUserId).NotEmpty();
    }
}

public sealed class LoginWithTelegramUserCommandHandler
    : IRequestHandler<LoginWithTelegramUserCommand, LogInResultModel>
{
    private readonly IMediator _mediator;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginWithTelegramUserCommandHandler(
        IMediator mediator,
        IJwtTokenService jwtTokenService)
    {
        _mediator = mediator;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LogInResultModel> Handle(LoginWithTelegramUserCommand command, CancellationToken token)
    {
        var telegramUser = await _mediator.Send(new GetTelegramUserQuery(command.TelegramUserId), token);
        if (telegramUser == null)
        {
            return LogInResultModel.FailResult(FailureReason.UserNotFound);
        }

        var accessToken = _jwtTokenService.GenerateToken(new() { UserId = telegramUser.UserId });

        return new LogInResultModel
        {
            Succeeded = true,
            AccessToken = accessToken
        };
    }
}