using System;

namespace Domain.Entities.NewFolder;

public sealed class TelegramUser
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public long OriginalTelegramUserId { get; init; }
    public long TelegramChatId { get; init; }
    public string TelegramUserName { get; init; }
    public string TelegramUserFirstName { get; init; }
    public string TelegramUserLastName { get; init; }
    public string LanguageCode { get; init; }
    public string TelegramBotToken { get; init; } = default!;
    public DateTime CreatedDate { get; init; }

    public TelegramBotConfiguration TelegramBotConfiguration { get; init; } = default!;
}