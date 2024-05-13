namespace Application.TelegramBot.Common.Models;

public sealed class MessageButtonModel
{
    public int Column { get; init; }
    public int Row { get; init; }
    public string Text { get; init; } = default!;
    public string CalbackData { get; init; } = default!;
}