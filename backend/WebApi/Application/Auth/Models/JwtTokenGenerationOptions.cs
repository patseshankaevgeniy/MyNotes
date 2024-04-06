using System;

namespace Application.Auth.Models;

public sealed class JwtTokenGenerationOptions
{
    public Guid UserId { get; init; }
}