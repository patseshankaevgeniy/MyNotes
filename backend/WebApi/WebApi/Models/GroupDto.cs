using System;
using System.Collections.Generic;
using WebApi.Models.Auth;

namespace WebApi.Models;

public sealed class GroupDto
{
    public Guid? Id { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; set; } = default!;
}
