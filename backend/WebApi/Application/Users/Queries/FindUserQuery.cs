using Application.Common.Interfaces;
using Application.Users.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries;

public sealed class FindUserQuery : IRequest<UserModel>
{
    public Guid? Id { get; init; }
    public string Email { get; init; }
}

public sealed class FindUserQueryHandler : IRequestHandler<FindUserQuery, UserModel?>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;

    public FindUserQueryHandler(
        IMapper mapper,
        IApplicationDbContext db)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<UserModel?> Handle(FindUserQuery query, CancellationToken token)
    {
        var dbQuery = _db.Users
            .AsNoTracking()
            .AsQueryable();

        if (query.Id.HasValue)
        {
            dbQuery = dbQuery.Where(u => u.Id == query.Id);
        }

        if (!string.IsNullOrEmpty(query.Email))
        {
            dbQuery = dbQuery.Where(u => u.Email == query.Email);
        }

        var user = await dbQuery
            .FirstOrDefaultAsync();

        return _mapper.Map<UserModel?>(user);
    }
}