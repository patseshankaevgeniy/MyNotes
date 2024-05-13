using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Handlers;

public sealed class UnkmownUpdateHandler : IUpdateHandler
{
    private readonly ILogger _logger;

    public UpdateType UpdateType => UpdateType.Unknown;

    public UnkmownUpdateHandler(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<UnkmownUpdateHandler>();
    }

    public Task HandleAsync(Update update, CancellationToken token)
    {
        _logger.LogWarning("We can't handle the update.", update);
        return Task.CompletedTask;
    }
}