using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.NewFolder;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.Auth.Commands;

public sealed class DeleteTelegramAuthCodeCommand : IRequest<Unit>
{
    public Guid UserId { get; init; }
}

public sealed class DeleteTelegramAuthCodeCommandValidator : AbstractValidator<DeleteTelegramAuthCodeCommand>
{
    public DeleteTelegramAuthCodeCommandValidator()
    {
        RuleFor(t => t.UserId).NotEmpty();
    }
}

public sealed class DeleteTelegramAuthCodeCommandHandler : IRequestHandler<DeleteTelegramAuthCodeCommand, Unit>
{
    private readonly IApplicationDbContext _db;

    public DeleteTelegramAuthCodeCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(DeleteTelegramAuthCodeCommand command, CancellationToken token)
    {
        var authCode = await _db.TelegramAuthCodes
            .SingleOrDefaultAsync(t => t.UserId == command.UserId, token);

        if (authCode == null)
        {
            throw new NotFoundException(nameof(TelegramAuthCode), $"userId: {command.UserId}");
        }

        _db.TelegramAuthCodes.Remove(authCode);
        await _db.SaveChangesAsync(token);

        return Unit.Value;
    }
}