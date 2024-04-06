namespace Application.Auth.Models;

public enum FailureReason
{
    UserNotFound = 0,
    WrongPassword = 1,
    UnknownReason = 2,
    ExistingUser = 3
}