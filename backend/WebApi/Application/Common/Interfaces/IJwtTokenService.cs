using Application.Auth.Models;

namespace Application.Common.Interfaces;

public interface IJwtTokenService
{
    public string GenerateToken(JwtTokenGenerationOptions options);
}