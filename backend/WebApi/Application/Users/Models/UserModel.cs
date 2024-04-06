using System;

namespace Application.Users.Models;

public sealed class UserModel
{
    public Guid UserId { get; set; }
    public bool HasMembers { get; init; }
    public string FirstName { get; init; } = default!;
    public string SecondName { get; init; } = default!;
    public Guid LanguageId { get; init; } = default!;
    public string Email { get; init; } = default!;
    public Guid ImageId { get; set; } = default!;
    public DateTime CratedDate { get; init; }
    public DateTime EditedDate { get; init; }
}