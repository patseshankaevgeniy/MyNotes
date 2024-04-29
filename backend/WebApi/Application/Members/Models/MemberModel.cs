using System;

namespace Application.Members.Models;

public sealed class MemberModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; init; } = default!;
    public string SecondName { get; init; } = default!;
    public string Email { get; set; } = default!;
    //public Guid ImageId { get; init; } = default!;
    public MemberStatus Status { get; init; }
}