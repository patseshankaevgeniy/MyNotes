using System;
using System.Collections.Generic;

namespace Application.Groups.Models;

public sealed class GroupModel
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; set; } = default!;
    public DateTime CreateDate { get; init; }
    public DateTime? ModifiedDate { get; init; }
}
