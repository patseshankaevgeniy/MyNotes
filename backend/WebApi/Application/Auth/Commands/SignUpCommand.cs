using Application.Auth.Models;
using Application.Users.Commands;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands;

public sealed class SignUpCommand : IRequest<LogInResultModel>
{
    public string Email { get; init; }
    public string UserName { get; init; }
    public string? FirstName { get; init; }
    public string? SecondName { get; init; }
    public string Password { get; init; }
}

public sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(t => t.Email).NotEmpty();
        RuleFor(t => t.UserName).NotEmpty();
        RuleFor(t => t.Password).NotEmpty();
    }
}

public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, LogInResultModel>
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;

    public SignUpCommandHandler(
        IMediator mediator,
        UserManager<User> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    public async Task<LogInResultModel> Handle(SignUpCommand command, CancellationToken token)
    {
        var existUser = await _userManager.FindByEmailAsync(command.Email);

        if (existUser != null)
        {
            return LogInResultModel.FailResult(FailureReason.ExistingUser);
        }

        var createUserCommand = new CreateUserCommand
        {
            Email = command.Email,
            UserName = command.UserName,
            FirstName = command.SecondName ?? string.Empty,
            SecondName = command.SecondName ?? string.Empty,
            Password = command.Password
        };
        await _mediator.Send(createUserCommand, token);

        var loginCommand = new LogInCommand
        {
            Email = command.Email,
            Password = command.Password
        };
        var loginResult = await _mediator.Send(loginCommand, token);

        return loginResult;
    }
}