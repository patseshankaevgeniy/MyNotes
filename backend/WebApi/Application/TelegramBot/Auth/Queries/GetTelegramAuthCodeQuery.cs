using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TelegramBot.Auth.Models;
using AutoMapper;
using Domain.Entities.NewFolder;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.Auth.Queries;

public sealed class GetTelegramAuthCodeQuery : IRequest<TelegramAuthCodeModel>
{
}

public sealed class GetTelegramAuthCodeQueryHandler : IRequestHandler<GetTelegramAuthCodeQuery, TelegramAuthCodeModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public GetTelegramAuthCodeQueryHandler(
        IMapper mapper,
        IApplicationDbContext db,
        ICurrentUserService currentUserService)
    {
        _db = db;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<TelegramAuthCodeModel> Handle(GetTelegramAuthCodeQuery request, CancellationToken token)
    {
        var authCode = await _db.TelegramAuthCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.UserId == _currentUserService.UserId, token);

        if (authCode == null)
        {
            throw new NotFoundException(nameof(TelegramAuthCode), $"userId: {_currentUserService.UserId}");
        }

        var model = _mapper.Map<TelegramAuthCodeModel>(authCode);
        return model;
    }
}