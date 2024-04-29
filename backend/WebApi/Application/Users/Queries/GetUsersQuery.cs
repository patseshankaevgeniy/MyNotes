using Application.Common.Interfaces;
using Application.Users.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries;

public sealed record GetUsersQuery : IRequest<IEnumerable<UserModel>>
{
    public bool? HasMembers { get; init; }
    public string SearchPattern { get; init; }
    public bool? OnlyCurrentUserMembers { get; init; }
}

public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public GetUsersQueryHandler(
        IMapper mapper,
        IApplicationDbContext db,
        ICurrentUserService currentUserService)
    {
        _db = db;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<UserModel>> Handle(GetUsersQuery query, CancellationToken token)
    {
        var dbQuery = _db.Users
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(query.SearchPattern))
        {
            dbQuery = dbQuery.Where(u =>
                u.FirstName.Contains(query.SearchPattern) ||
                u.SecondName.Contains(query.SearchPattern) ||
                u.Email.Contains(query.SearchPattern));
        }

        if (query.OnlyCurrentUserMembers.HasValue)
        {
            dbQuery = dbQuery
                .Where(u => _currentUserService.UserGroupMembersIds.Contains(u.Id));
        }

        if (query.HasMembers.HasValue)
        {
            dbQuery = query.HasMembers.Value
                ? dbQuery.Where(u => u.Group != null)
                : dbQuery.Where(u => u.Group == null);
        }

        var users = await dbQuery
            .OrderBy(u => u.UserName)
            .ToListAsync(token);

        return _mapper.Map<IEnumerable<UserModel>>(users);
    }
}