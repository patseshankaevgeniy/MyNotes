using System;

namespace Application.TelegramBot.TelegramUser.Models;

public sealed class TelegramUserModel
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
}