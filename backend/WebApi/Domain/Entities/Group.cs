using System;
using System.Collections.Generic;

namespace Domain.Entities;

public sealed class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime CratedDate { get; set; }
    public DateTime EditedDate { get; set; }

    public ICollection<User> Users { get; set; } = default!;
}