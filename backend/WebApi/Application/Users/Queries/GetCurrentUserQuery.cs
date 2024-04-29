using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Users.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries;

public sealed record GetCurrentUserQuery : IRequest<UserModel>;

public sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentUserQueryHandler(
        IMapper mapper,
        IApplicationDbContext db,
        ICurrentUserService currentUserService)
    {
        _db = db;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<UserModel> Handle(GetCurrentUserQuery query, CancellationToken token)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == _currentUserService.UserId, token);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), _currentUserService.UserId);
        }

        return _mapper.Map<UserModel>(user);
    }
}