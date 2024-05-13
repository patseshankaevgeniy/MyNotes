using System;

namespace Application.TelegramBot.Auth.Models;

public sealed class TelegramAuthCodeModel
{
    public string LinkCode { get; init; } = default!;

    public int ShortCode { get; init; }
    public DateTime ShortCodeExpirationDate { get; init; }
}