namespace Application.TelegramBot.Auth.Models;

public enum TelegramAuthResult
{
    None,
    Succeeded,
    FirstLogin,
    UserNotFound,
    CodeNotValid,
}