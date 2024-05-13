using System;

namespace Domain.Entities.NewFolder;

public sealed class TelegramSessionMessage
{
    public Guid Id { get; init; }
    public Guid SessionId { get; set; }
    public Guid UserId { get; init; }
    public int MessageId { get; init; }
    public TelegramSessionMessageType Type { get; set; }
    public TelegramMessageState State { get; set; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; set; }

    public TelegramSession Session { get; init; } = default!;
}