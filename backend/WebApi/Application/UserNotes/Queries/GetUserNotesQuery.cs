using Application.Common.Interfaces;
using Application.UserNotes.Commands;
using Application.UserNotes.Models;
using AutoMapper;
using Domain.Entities;
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
    private readonly IDateTimeService _dateTimeService;
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public GetUserNotesQueryHandler(
        ICurrentUserService currentUserService,
        IApplicationDbContext db,
        IMapper mapper,
        IDateTimeService dateTimeService)
    {
        _currentUserService = currentUserService;
        _db = db;
        _mapper = mapper;
        _dateTimeService = dateTimeService;
    }

    public async Task<IEnumerable<UserNoteModel>> Handle(GetUserNotesQuery query, CancellationToken cancellationToken)
    {
        var actualUserNotes = new List<UserNote>();
        var userNotes = await _db.UserNotes
                .AsNoTracking()
                .Where(n => n.UserId == _currentUserService.UserId && n.IsActual)
                .ToListAsync(cancellationToken);

        foreach (var userNote in userNotes)
        {
            if (userNote.Сompletion < _dateTimeService.UtcNow)
            {
                var command = new DeleteUserNoteCommand(userNote.Id);
            }
            else
            {
                actualUserNotes.Add(userNote);
            }
        }

        var models = _mapper.Map<IEnumerable<UserNoteModel>>(actualUserNotes);
        return models;
    }
}
