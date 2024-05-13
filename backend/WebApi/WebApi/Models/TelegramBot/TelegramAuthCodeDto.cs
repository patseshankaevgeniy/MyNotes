using System;

namespace WebApi.Models.TelegramBot;

public sealed class TelegramAuthCodeDto
{
    public string Link { get; set; } = default!;
    public string LinkCode { get; init; } = default!;

    public int ShortCode { get; set; }
    public DateTime ShortExpirationDate { get; set; }
}