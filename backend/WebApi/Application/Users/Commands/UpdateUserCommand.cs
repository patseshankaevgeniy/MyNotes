using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Users.Models;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands;

public sealed class UpdateUserCommand : IRequest<UserModel>
{
    public string FirstName { get; init; }
    public string SecondName { get; init; }
    public string Email { get; init; }
    //public Guid ImageId { get; init; }
    public Guid LanguageId { get; init; }
}

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty();
        RuleFor(u => u.SecondName).NotEmpty();
        RuleFor(u => u.Email).NotEmpty();
       // RuleFor(u => u.ImageId).NotEmpty();
        RuleFor(u => u.LanguageId).NotEmpty();
    }
}

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly IMessageService _messageService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserCommandHandler(
        IMapper mapper,
        IApplicationDbContext db,
        UserManager<User> userManager,
        IMessageService messageService,
        ICurrentUserService userService,
        IDateTimeService dateTimeService)
    {
        _db = db;
        _mapper = mapper;
        _userManager = userManager;
        _messageService = messageService;
        _currentUserService = userService;
        _dateTimeService = dateTimeService;
    }

    public async Task<UserModel> Handle(UpdateUserCommand command, CancellationToken token)
    {
        var user = await _db.Users.FirstOrDefaultAsync(t => t.Id == _currentUserService.UserId, token);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), _currentUserService.UserId);
        }

        user.Email = command.Email!;
        user.FirstName = command.FirstName!;
        user.SecondName = command.SecondName!;
        user.LanguageId = command.LanguageId;
        //user.ImageId = command.ImageId;
        user.EditedDate = _dateTimeService.Now;

        await _userManager.UpdateAsync(user);
        await _messageService.SendUserUpdatedAsync();

        return _mapper.Map<UserModel>(user);
    }
}