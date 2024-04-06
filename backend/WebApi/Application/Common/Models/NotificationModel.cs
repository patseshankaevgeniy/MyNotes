using System;

namespace Application.Common.Models;

public sealed class NotificationModel
{
    public NotificationType Type { get; set; }
    public Guid UserId { get; set; }
}
