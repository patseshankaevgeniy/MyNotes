using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Users.Models;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands;

public sealed class CreateUserCommand : IRequest<UserModel>
{
    public Guid? Id { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string SecondName { get; init; }
    public Guid? LanguageId { get; init; }
    public Guid? CurrencyId { get; init; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid? ImageId { get; set; }
}

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.UserName).NotEmpty();
        RuleFor(u => u.FirstName).NotEmpty();
        RuleFor(u => u.Email).NotEmpty();
    }
}

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserModel>
{
    private readonly IMapper _mapper;
    private readonly IGuidService _guidService;
    private readonly UserManager<User> _userManager;
    //private readonly IMessageService _messageService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IConstantsService _constantsService;

    public CreateUserCommandHandler(
        IMapper mapper,
        IGuidService guidService,
        UserManager<User> userManager,
       // IMessageService messageService,
        IDateTimeService dateTimeService,
        IConstantsService constantsService)
    {
        _mapper = mapper;
        _guidService = guidService;
        _userManager = userManager;
       // _messageService = messageService;
        _dateTimeService = dateTimeService;
        _constantsService = constantsService;
    }

    public async Task<UserModel> Handle(CreateUserCommand command, CancellationToken token)
    {
        var user = new User
        {
            Id = command.Id ?? _guidService.NewGuid(),
            Email = command.Email!,
            UserName = command.UserName!,
            FirstName = command.FirstName!,
            SecondName = command.SecondName ?? string.Empty,
            LanguageId = command.LanguageId ?? _constantsService.DefaultLanguageId,
            ImageId = command.ImageId ?? _constantsService.DefaultImageId,
            CratedDate = _dateTimeService.Now
        };

        var result = await _userManager.CreateAsync(user, command.Password ?? _constantsService.DefaultPassword);
        if (!result.Succeeded)
        {
            var errors = JsonConvert.SerializeObject(result.Errors);
            throw new BadRequestException($"Some error during creation of user: {errors}");
        }

        //await _messageService.SendUserCreatedAsync(user.Id);

        return _mapper.Map<UserModel>(user);
    }
}