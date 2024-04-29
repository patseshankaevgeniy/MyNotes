using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserNotes.Commands;

public sealed record class DeleteUserNoteCommand(Guid? Id) : IRequest<Unit>;

public sealed class DeleteUserNoteCommandValidator : AbstractValidator<DeleteUserNoteCommand>
{
    public DeleteUserNoteCommandValidator()
    {
        RuleFor(n => n.Id).NotEmpty();
    }
}

public sealed class DeleteUserNoteCommandHandler: IRequestHandler<DeleteUserNoteCommand, Unit>
{
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public DeleteUserNoteCommandHandler(
        IApplicationDbContext db, 
        ICurrentUserService currentUserService)
    {
        _db = db;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteUserNoteCommand command, CancellationToken cancellationToken)
    {
        var userNote = await _db.UserNotes.FirstOrDefaultAsync(n => n.Id == command.Id);
        if (userNote == null)
        {
            throw new NotFoundException(nameof(UserNote), command.Id!);
        }

        _db.UserNotes.Remove(userNote);
        await _db.SaveChangesAsync();

        return Unit.Value;
    }
}

