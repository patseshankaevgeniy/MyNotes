using Application.Common.Interfaces;
using Application.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public sealed class CurrentUserService : ICurrentUserService, ICurrentUserInitializer
{
    private readonly IUsersCacheService _usersCacheService;

    public Guid UserId => User?.UserId ?? throw new Exception($"{nameof(CurrentUserService)} not initialized");
    public UserModel User { get; private set; } = default!;

    public IEnumerable<Guid> UserGroupMembersIds { get; private set; } = default!;
    public bool UserHasMembers => UserGroupMembersIds.Any();

    Guid ICurrentUserService.UserId => throw new NotImplementedException();

    UserModel ICurrentUserService.User => throw new NotImplementedException();

    IEnumerable<Guid> ICurrentUserService.UserGroupMembersIds => throw new NotImplementedException();

    public CurrentUserService(IUsersCacheService usersCacheService)
    {
        _usersCacheService = usersCacheService;
    }

    public async Task InitializeAsync(Guid userId)
    {
        var userWithMembers = await _usersCacheService.GetCurrentUserWithMembersAsync(userId);
        User = userWithMembers.UserModel;
        UserGroupMembersIds = userWithMembers.UserGroupMembersIds;
    }
}