namespace WebApi.Models.Auth;

public sealed class LogInResultDto
{
    public bool Succeeded { get; set; }
    public string AccessToken { get; set; } = default!;
    public int? FailureReason { get; set; }
}