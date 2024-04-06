using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using SignalR.HubConfig;
using SignalR.Options;
using System.Threading.Tasks;

namespace SignalR.Services;

public sealed class NotificationService : INotificationService
{
    private readonly SignalROptions _options;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(
        IOptions<SignalROptions> options,
        IHubContext<NotificationHub> hubContext)
    {
        _options = options.Value;
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(NotificationModel model)
    {
        await _hubContext.Clients.All.SendAsync(_options.NotificationMethodName, model);
    }
}