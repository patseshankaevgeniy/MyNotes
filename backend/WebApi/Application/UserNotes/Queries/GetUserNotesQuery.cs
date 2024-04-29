using Application.Common.Interfaces;
using Application.UserNotes.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserNotes.Queries;

public sealed record GetUserNotesQuery : IRequest<IEnumerable<UserNoteModel>>;


public sealed class GetUserNotesQueryHandler : IRequestHandler<GetUserNotesQuery, IEnumerable<UserNoteModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public GetUserNotesQueryHandler(
        ICurrentUserService currentUserService,
        IApplicationDbContext db,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserNoteModel>> Handle(GetUserNotesQuery query, CancellationToken cancellationToken)
    {
        var userNotes = await _db.UserNotes
                .AsNoTracking()
                .Where(n => n.UserId == _currentUserService.UserId)
                .ToListAsync(cancellationToken);

        var models = _mapper.Map<IEnumerable<UserNoteModel>>(userNotes);
        return models;
    }
}
