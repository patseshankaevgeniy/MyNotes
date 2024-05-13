using Application.Common.Interfaces;
using Application.TelegramBot.Auth.Commands;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Application.Common.Services;

public sealed class MessageService : IMessageService
{
    private readonly IMediator _mediator;
    private readonly IUsersCacheService _usersCacheService;
    private readonly INotificationService _notificationService;

    public MessageService(
        IMediator mediator,
        IUsersCacheService usersCacheService,
        INotificationService notificationService)
    {
        _mediator = mediator;
        _usersCacheService = usersCacheService;
        _notificationService = notificationService;
    }

    public async Task SendUserCreatedAsync(Guid userId)
    {
        await _usersCacheService.ResetCacheAsync();
        await _mediator.Send(new CreateTelegramAuthCodeCommand(userId));
    }

    public async Task SendUserUpdatedAsync()
    {
        await _usersCacheService.ResetCacheAsync();
    }

    public async Task SendUserMemberAcceptedAsync(Guid userId)
    {
        await _usersCacheService.ResetCacheAsync();
        await _notificationService.SendNotificationAsync(new()
        {
            Type = Models.NotificationType.MemberStatusChanged,
            UserId = userId
        });
    }

    public async Task SendUserMemberDeclinedAsync(Guid userId)
    {
        await _usersCacheService.ResetCacheAsync();
        await _notificationService.SendNotificationAsync(new()
        {
            Type = Models.NotificationType.MemberStatusChanged,
            UserId = userId
        });
    }

    public async Task SendOperationCreatedAsync(Guid purchaseId, Guid userId)
    {
        await _notificationService.SendNotificationAsync(new()
        {
            Type = Models.NotificationType.PurchasesUpdate,
            UserId = userId
        });
    }

    public async Task SendTelegramUserCreatedAsync(Guid telegramUserId, Guid userId)
    {
        await _notificationService.SendNotificationAsync(new()
        {
            Type = Models.NotificationType.TelegramBotConnected,
            UserId = userId
        });
    }
}