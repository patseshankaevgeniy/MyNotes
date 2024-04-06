using System;

namespace Domain.Entities;

public sealed class MembershipStatus
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public AcceptanceStatus Status { get; set; }
    public bool? IsInviter { get; set; }

    public User User { get; set; } = default!;
}
