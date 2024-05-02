using Application.Auth.Models;
using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands;

public sealed class LogInCommand : IRequest<LogInResultModel>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public sealed class LogInCommandValidator : AbstractValidator<LogInCommand>
{
    public LogInCommandValidator()
    {
        RuleFor(t => t.Email).NotEmpty();
        RuleFor(t => t.Password).NotEmpty();
    }
}

public sealed class LogInCommandHandler : IRequestHandler<LogInCommand, LogInResultModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly SignInManager<User> _signInManager;

    public LogInCommandHandler(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LogInResultModel> Handle(LogInCommand command, CancellationToken token)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null)
        {
            return LogInResultModel.FailResult(FailureReason.UserNotFound);
        }

        if (!await _userManager.CheckPasswordAsync(user, command.Password))
        {
            return LogInResultModel.FailResult(FailureReason.WrongPassword);
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, command.Password, true, false);
        if (!signInResult.Succeeded)
        {
            return LogInResultModel.FailResult(FailureReason.UnknownReason);
        }

        return new LogInResultModel
        {
            Succeeded = true,
            AccessToken = _jwtTokenService.GenerateToken(new() { UserId = user.Id })
        };
    }
}