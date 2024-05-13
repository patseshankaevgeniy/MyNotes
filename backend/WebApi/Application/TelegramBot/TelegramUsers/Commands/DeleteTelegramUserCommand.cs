using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.TelegramUsers.Commands;

public sealed class DeleteTelegramUserCommand : IRequest<Unit>
{
}

public sealed class DeleteTelegramUserCommandHandler : IRequestHandler<DeleteTelegramUserCommand, Unit>
{
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public DeleteTelegramUserCommandHandler(
        IApplicationDbContext db,
        ICurrentUserService currentUserService)
    {
        _db = db;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteTelegramUserCommand command, CancellationToken token)
    {
        var telegramUser = await _db.TelegramUsers
            .FirstOrDefaultAsync(t => t.UserId == _currentUserService.UserId);

        if (telegramUser == null)
        {
            throw new NotFoundException(nameof(TelegramUser), $"userId: {_currentUserService.UserId}");
        }

        _db.TelegramUsers.Remove(telegramUser);
        await _db.SaveChangesAsync();

        return Unit.Value;
    }
}