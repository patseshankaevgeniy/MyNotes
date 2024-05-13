using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Recevier;

public interface IUpdateReceiver
{
    Task ReceiveUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token);
    Task ReceiveErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token);
}