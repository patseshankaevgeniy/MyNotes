using Application.Common.Interfaces;
using Application.TelegramBot.Auth.Models;
using Application.Users.Queries;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using static Application.TelegramBot.Auth.Models.ValidationFailReason;

namespace Application.TelegramBot.Auth.Commands;

public sealed record ValidateTelegramAuthCodeCommand(string Text) : IRequest<TelegramAuthCodeValidationResultModel>;

public sealed class ValidateTelegramAuthCodeCommandValidator : AbstractValidator<ValidateTelegramAuthCodeCommand>
{
    public ValidateTelegramAuthCodeCommandValidator()
    {
        RuleFor(t => t.Text).NotEmpty();
    }
}

public sealed class ValidateTelegramAuthCodeCommandHandler
    : IRequestHandler<ValidateTelegramAuthCodeCommand, TelegramAuthCodeValidationResultModel>
{
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _db;
    private readonly IDateTimeService _dateTimeService;

    public ValidateTelegramAuthCodeCommandHandler(
        IMediator mediator,
        IApplicationDbContext db,
        IDateTimeService dateTimeService)
    {
        _mediator = mediator;
        _db = db;
        _dateTimeService = dateTimeService;
    }

    public async Task<TelegramAuthCodeValidationResultModel> Handle(ValidateTelegramAuthCodeCommand command, CancellationToken token)
    {
        if (int.TryParse(command.Text, out int code))
        {
            var authCode = await _db.TelegramAuthCodes
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.ShortCode == code, token);

            if (authCode == null)
            {
                return TelegramAuthCodeValidationResultModel.CreateFailedResult(CodeNotFound);
            }

            if (authCode.ShortExpirationDate < _dateTimeService.UtcNow)
            {
                return TelegramAuthCodeValidationResultModel.CreateFailedResult(CodeExpiered);
            }

            var currentUserModel = await _mediator.Send(new FindUserQuery { Id = authCode.UserId }, token);
            return new() { Succeeded = true, CurrentUser = currentUserModel };
        }
        else
        {
            var telegramAuthCode = await _db.TelegramAuthCodes
                .AsNoTracking()
                .FirstOrDefaultAsync(t => command.Text!.Contains(t.LinkCode), token);

            if (telegramAuthCode != null)
            {
                var currentUserModel = await _mediator.Send(new FindUserQuery { Id = telegramAuthCode.UserId }, token);
                return new() { Succeeded = true, CurrentUser = currentUserModel };
            }
        }

        return TelegramAuthCodeValidationResultModel.CreateFailedResult(CodeCantBeParsed);
    }
}