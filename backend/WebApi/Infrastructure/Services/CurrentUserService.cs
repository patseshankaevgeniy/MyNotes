using Application.Common.Interfaces;
using Application.Users.Models;
using Application.Users.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public sealed class CurrentUserService : ICurrentUserService, ICurrentUserInitializer
{
    private readonly IUsersCacheService _usersCacheService;
    private readonly IMediator _mediator;

    public Guid UserId => User?.UserId ?? throw new Exception($"{nameof(CurrentUserService)} not initialized");
    public UserModel User { get; private set; } = default!;

    public IEnumerable<Guid> UserGroupMembersIds { get; private set; } = default!;
    public bool UserHasMembers => UserGroupMembersIds.Any();

    public CurrentUserService(IUsersCacheService usersCacheService, IMediator mediator)
    {
        _usersCacheService = usersCacheService;
        _mediator = mediator;
    }

    public async Task InitializeAsync(Guid userId)
    {
        var userWithMembers = await _usersCacheService.GetCurrentUserWithMembersAsync(userId);
        if (userWithMembers != null)
        {
            User = userWithMembers.UserModel;
            UserGroupMembersIds = userWithMembers.UserGroupMembersIds;
        }
        else
        {
            var userWithoutMembers = await _mediator.Send(new GetUserQuery { Id = userId }) ;
            User = userWithoutMembers;
        }
    }
}