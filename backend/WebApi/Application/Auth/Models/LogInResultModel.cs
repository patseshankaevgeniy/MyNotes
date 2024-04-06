namespace Application.Auth.Models;

public sealed class LogInResultModel
{
    public bool Succeeded { get; set; }
    public string AccessToken { get; set; } = default!;
    public FailureReason? FailureReason { get; set; }

    public static LogInResultModel FailResult(FailureReason reason) => new()
    {
        Succeeded = false,
        FailureReason = reason
    };
}