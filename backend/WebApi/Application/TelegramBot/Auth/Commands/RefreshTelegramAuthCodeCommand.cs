using Application.Common.Interfaces;
using Application.TelegramBot.Auth.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.Auth.Commands;

public sealed record RefreshTelegramAuthCodeCommand : IRequest<TelegramAuthCodeModel>;

public sealed class RefreshTelegramAuthCodeCommandHandler : IRequestHandler<RefreshTelegramAuthCodeCommand, TelegramAuthCodeModel>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _db;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public RefreshTelegramAuthCodeCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IApplicationDbContext db,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _db = db;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public async Task<TelegramAuthCodeModel> Handle(RefreshTelegramAuthCodeCommand command, CancellationToken token)
    {
        var telegramAuthCode = await _db.TelegramAuthCodes
            .Where(t => t.UserId == _currentUserService.UserId)
            .FirstOrDefaultAsync(token);

        if (telegramAuthCode == null)
        {
            return await _mediator.Send(new CreateTelegramAuthCodeCommand(_currentUserService.UserId));
        }

        telegramAuthCode.ShortCode = new Random().Next(1_000, 9_999);
        telegramAuthCode.ShortExpirationDate = _dateTimeService.UtcNow.AddHours(2);

        await _db.SaveChangesAsync(token);

        return _mapper.Map<TelegramAuthCodeModel>(telegramAuthCode);
    }
}