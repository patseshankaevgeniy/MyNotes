using Application.Users.Models;
using System;
using System.Collections.Generic;

namespace Application.Common.Interfaces;

public interface ICurrentUserService
{
    public Guid UserId { get; }
    public UserModel User { get; }
    public bool UserHasMembers { get; }
    public IEnumerable<Guid> UserGroupMembersIds { get; }
}