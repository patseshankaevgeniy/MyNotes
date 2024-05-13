using System;

namespace Domain.Entities.NewFolder;

public sealed class TelegramMessageLog
{
    public int TelegramMessageId { get; init; }
    public long TelegramChatId { get; init; }
    public long TelegramUserId { get; init; }
    public Guid? UserId { get; init; }
    public BotMessageType Type { get; init; }
    public string Text { get; init; }
    public bool IsBot { get; init; }
    public DateTime Date { get; init; }
}