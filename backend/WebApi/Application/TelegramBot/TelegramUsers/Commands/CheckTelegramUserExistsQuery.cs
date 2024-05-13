using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.TelegramUsers.Commands;

public sealed record CheckTelegramUserExistsQuery : IRequest<bool>;

public sealed class CheckTelegramUserExistsQueryHandler : IRequestHandler<CheckTelegramUserExistsQuery, bool>
{
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public CheckTelegramUserExistsQueryHandler(
        IApplicationDbContext db,
        ICurrentUserService currentUserService)
    {
        _db = db;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(CheckTelegramUserExistsQuery query, CancellationToken token)
    {
        var telegarmUserExists = await _db.TelegramUsers
            .AnyAsync(t => t.UserId == _currentUserService.UserId);

        return telegarmUserExists;
    }
}