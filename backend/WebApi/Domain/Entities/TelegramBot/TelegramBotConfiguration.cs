using System;

namespace Domain.Entities.NewFolder;

public sealed class TelegramBotConfiguration
{
    public Guid Id { get; set; }
    public Guid TelegramUserId { get; set; }
    public bool ReceiveMemberNotesNotification { get; set; }


    public TelegramUser TelegramUser { get; init; } = default!;
}