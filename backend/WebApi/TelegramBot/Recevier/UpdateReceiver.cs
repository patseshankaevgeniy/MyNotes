using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Handlers;

namespace TelegramBot.Recevier;

public sealed class UpdateReceiver : IUpdateReceiver
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;

    public UpdateReceiver(
        ILoggerFactory loggerFactory,
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = loggerFactory.CreateLogger<UpdateReceiver>();
    }

    public async Task ReceiveUpdateAsync(ITelegramBotClient _, Update update, CancellationToken token)
    {
        var stopwatch = new Stopwatch();
        using var scope = _serviceProvider.CreateScope();

        try
        {
            var updatesHandlerFactory = scope.ServiceProvider.GetRequiredService<IUpdateHandlerFactory>();
            var updatesHandler = updatesHandlerFactory.Create(update.Type);

            _logger.LogInformation($"- [{nameof(UpdateReceiver)}] received update [{update.Type}] from telegram bot.");
            stopwatch.Start();

            await updatesHandler.HandleAsync(update, token);

            stopwatch.Stop();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Can't handle update from telegram bot: {ex.Message}");
            throw;
        }

        _logger.LogInformation($"- [{nameof(UpdateReceiver)}] handled update [{update.Type}] from telegram bot, duration: {stopwatch.ElapsedMilliseconds} ms");
    }

    public async Task ReceiveErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token)
    {
        _logger.LogError(exception, $"Can't handle exception from telegram bot: {exception.Message}");
        await Task.CompletedTask;
    }
}