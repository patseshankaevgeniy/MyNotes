using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using IUpdateReceiver = TelegramBot.Recevier.IUpdateReceiver;

namespace TelegramBot;

public sealed class TelegramWorkerService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly ITelegramBotClient _client;
    private readonly IUpdateReceiver _updateReceiver;

    public TelegramWorkerService(
        IConfiguration configuration,
        ILoggerFactory loggerFactory,
        IUpdateReceiver updateReceiver)
    {
        _updateReceiver = updateReceiver;
        _logger = loggerFactory.CreateLogger<TelegramWorkerService>();
        _client = new TelegramBotClient(configuration["TelegramToken"]);
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message,
                UpdateType.CallbackQuery
            }
        };

        await _client.DeleteWebhookAsync();

        _client.StartReceiving(
            updateHandler: _updateReceiver.ReceiveUpdateAsync,
            pollingErrorHandler: _updateReceiver.ReceiveErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: token);

        _logger.LogInformation($"- {nameof(TelegramWorkerService)} started receiving updates from telegram bot");
    }
}