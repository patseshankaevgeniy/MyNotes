using Application.Users.Models;

namespace Application.TelegramBot.Auth.Models;

public sealed class TelegramAuthCodeValidationResultModel
{
    public bool Succeeded { get; init; }
    public UserModel CurrentUser { get; init; } = default!;
    public ValidationFailReason? FailReason { get; init; }

    public static TelegramAuthCodeValidationResultModel CreateFailedResult(ValidationFailReason? failReason)
    {
        return new()
        {
            Succeeded = false,
            FailReason = failReason
        };
    }
}

public enum ValidationFailReason
{
    CodeNotFound = 1,
    CodeExpiered = 2,
    CodeCantBeParsed = 3
}