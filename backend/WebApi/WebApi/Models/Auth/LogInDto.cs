namespace WebApi.Models.Auth;

public sealed class LogInDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}