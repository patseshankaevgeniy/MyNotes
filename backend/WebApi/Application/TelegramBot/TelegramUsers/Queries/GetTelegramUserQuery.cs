using Application.Common.Interfaces;
using Application.TelegramBot.TelegramUser.Models;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TelegramBot.TelegramUsers.Queries;

public sealed record GetTelegramUserQuery(long TelegramUserId) : IRequest<TelegramUserModel>;

public sealed class GetTelegramUserQueryValidator : AbstractValidator<GetTelegramUserQuery>
{
    public GetTelegramUserQueryValidator()
    {
        RuleFor(t => t.TelegramUserId).NotEmpty();
    }
}

public sealed class GetTelegramUserQueryHandler : IRequestHandler<GetTelegramUserQuery, TelegramUserModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;
    private readonly IConfiguration _configuration;

    public GetTelegramUserQueryHandler(
        IMapper mapper,
        IApplicationDbContext db,
        IConfiguration configuration)
    {
        _db = db;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<TelegramUserModel> Handle(GetTelegramUserQuery query, CancellationToken token)
    {
        var telegramUser = await _db.TelegramUsers
            .AsNoTracking()
            .Where(t => t.OriginalTelegramUserId == query.TelegramUserId)
            .FirstOrDefaultAsync(token);

        return _mapper.Map<TelegramUserModel>(telegramUser);
    }
}