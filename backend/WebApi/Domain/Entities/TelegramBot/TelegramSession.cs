using System;
using System.Collections.Generic;

namespace Domain.Entities.NewFolder;

public sealed class TelegramSession
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public TelegramSessionType Type { get; init; }
    public TelegramSessionStatus Status { get; set; }
    public string State { get; set; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; set; }

    public List<TelegramSessionMessage> Messages { get; init; } = new();
}