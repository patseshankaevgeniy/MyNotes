using System;

namespace Domain.Entities.NewFolder;

public sealed class TelegramAuthCode
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string LinkCode { get; init; } = default!;
    public int ShortCode { get; set; }
    public DateTime ShortExpirationDate { get; set; }
}