using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Handlers;

public interface IUpdateHandler
{
    UpdateType UpdateType { get; }

    Task HandleAsync(Update update, CancellationToken token);
}