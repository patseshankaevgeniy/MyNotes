namespace Application.TelegramBot.Common.Models;

public class BotCommandResult
{
    public bool Succeeded { get; init; }
    public BotMessage Response { get; init; }

    public static BotCommandResult CreateSucceeded()
    {
        return new BotCommandResult
        {
            Succeeded = true
        };
    }
}