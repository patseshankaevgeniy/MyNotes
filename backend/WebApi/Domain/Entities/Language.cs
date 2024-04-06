using System;

namespace Domain.Entities;

public sealed class Language
{
    public Guid Id { get; set; }
    public string Value { get; set; } = default!;
}
