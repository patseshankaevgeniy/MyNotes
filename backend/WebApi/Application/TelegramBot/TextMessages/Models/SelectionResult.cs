using Application.TelegramBot.Common.Models;
using MediatR;

namespace Application.TelegramBot.TextMessages.Models;

public sealed class SelectionResult
{
    public bool Suits { get; init; }
    public int CompatibilityInPercent { get; init; }
    public IRequest<BotCommandResult> BotCommand { get; init; }

    public static SelectionResult CreateDoesntSuits() => new()
    {
        Suits = false,
        BotCommand = null,
        CompatibilityInPercent = 0
    };
}