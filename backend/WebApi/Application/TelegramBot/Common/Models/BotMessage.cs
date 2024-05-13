using Domain.Entities.NewFolder;
using System;

namespace Application.TelegramBot.Common.Models;

public sealed class BotMessage
{
    public int Id { get; init; }
    public string Text { get; init; }
    public string CallbackData { get; init; }
    public DateTime Date { get; init; }

    public BotMessageType Type { get; init; }
}