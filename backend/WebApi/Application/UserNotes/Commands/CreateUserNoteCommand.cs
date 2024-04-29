using Application.Common.Interfaces;
using Application.UserNotes.Models;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserNotes.Commands;

public sealed class CreateUserNoteCommand : IRequest<UserNoteModel>
{
    public string Text { get; init; }
    public Guid? CreatorId { get; init; }
    public NotePriority Priority { get; init; }
    public DateTime? Сompletion { get; set; }
}

public sealed class CreateUserNoteCommandValidator : AbstractValidator<CreateUserNoteCommand>
{
    public CreateUserNoteCommandValidator()
    {
        RuleFor(un => un.Text).NotEmpty();
        RuleFor(un => un.Priority).IsInEnum();
    }
}

public sealed class CreateUserNoteCommandHandler : IRequestHandler<CreateUserNoteCommand, UserNoteModel>
{
    private readonly IMapper _mapper;
    private readonly IGuidService _guidService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IApplicationDbContext _db;
    private readonly ICurrentUserService _currentUserService;

    public const bool IS_ACTUAL_USER_NOTE = true;
   

    public CreateUserNoteCommandHandler(
        IGuidService guidService,
        IMapper mapper,
        IDateTimeService dateTimeService,
        IApplicationDbContext db,
        ICurrentUserService currentUserService)
    {
        _guidService = guidService;
        _mapper = mapper;
        _dateTimeService = dateTimeService;
        _db = db;
        _currentUserService = currentUserService;
    }

    public async Task<UserNoteModel> Handle(CreateUserNoteCommand command, CancellationToken token)
    {
        var comletionNoteTime = _dateTimeService.SetCompetionNoteTime(command.Priority);

        var userNote = new UserNote
        {
            Id = _guidService.NewGuid(),
            Created = _dateTimeService.UtcNow,
            Text = command.Text,
            Priority = command.Priority,
            UserId = command.CreatorId ?? _currentUserService.UserId,
            IsActual = IS_ACTUAL_USER_NOTE,
            Сompletion = command.Сompletion ?? comletionNoteTime,
            
        };
        
        _db.UserNotes.Add(userNote);
        await _db.SaveChangesAsync(token);

        var model = _mapper.Map<UserNoteModel>(userNote);
        return model;
    }
}