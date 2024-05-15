using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Users.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries;

public sealed class GetUserQuery : IRequest<UserModel>
{
    public Guid Id { get; init; }
}

public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;

    public GetUserQueryHandler(
        IMapper mapper,
        IApplicationDbContext db)
    {
        _mapper = mapper;
        _db = db;
    }

    public async Task<UserModel> Handle(GetUserQuery query, CancellationToken token)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == query.Id, token);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), query.Id);
        }

        return _mapper.Map<UserModel>(user);
    }
}
