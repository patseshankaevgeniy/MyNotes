using System;

namespace WebApi.Models.Auth;

public sealed class UserDto
{
    public Guid UserId { get; init; }
    public bool HasMembers { get; init; }
    public string FirstName { get; init; } = default!;
    public string SecondName { get; init; } = default!;
    public Guid LanguageId { get; init; } = default!;
    public string Email { get; init; } = default!;
    public Guid ImageId { get; init; }
    public string ImageUrl { get; init; } = default!;
}