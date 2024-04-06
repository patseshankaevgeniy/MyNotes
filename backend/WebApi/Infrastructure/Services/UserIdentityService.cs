using Application.Auth.Models;
using Application.Common.Interfaces;
using Application.Users.Models;
using Application.Users.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public sealed class UserIdentityService : IUserIdentityService
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;

    public UserIdentityService(
        IMediator mediator,
        UserManager<User> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    public async Task<UserModel?> FindByLoginAsync(UserLoginInfoModel loginInfoModel)
    {
        var user = await _userManager.FindByLoginAsync(loginInfoModel.LoginProvider, loginInfoModel.ProviderKey);
        if (user == null)
        {
            return default;
        }

        return await _mediator.Send(new FindUserQuery { Id = user.Id });
    }

    public async Task LoginAsync(UserModel userModel, UserLoginInfoModel loginInfoModel)
    {
        var user = await _userManager.FindByIdAsync(userModel.UserId.ToString());

        var identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(
            loginInfoModel.LoginProvider,
            loginInfoModel.ProviderKey,
            loginInfoModel.ProviderDisplayName));

        if (identityResult.Succeeded)
        {
        }
    }
}