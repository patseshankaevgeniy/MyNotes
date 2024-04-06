using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;

public interface IMessageService
{
    Task SendUserCreatedAsync(Guid userId);
    Task SendUserUpdatedAsync();

    Task SendUserMemberAcceptedAsync(Guid userId);
    Task SendUserMemberDeclinedAsync(Guid userId);

    Task SendOperationCreatedAsync(Guid purchaseId, Guid userId);

   // Task SendTelegramUserCreatedAsync(Guid telegramUserId, Guid userId);
}