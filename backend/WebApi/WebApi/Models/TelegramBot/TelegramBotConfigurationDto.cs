using System;

namespace WebApi.Models.TelegramBot;

public sealed class TelegramBotConfigurationDto
{
    public Guid TelegramUserId { get; init; }
    public bool ReceiveMemberNoteNotification { get; init; }
    public TimeOnly DaySumupNotificationTime { get; init; }
}