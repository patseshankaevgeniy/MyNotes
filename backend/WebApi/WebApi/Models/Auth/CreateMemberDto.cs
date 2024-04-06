using System;

namespace WebApi.Models.Auth;

public sealed class CreateMemberDto
{
    public Guid? UserId { get; init; }
}
