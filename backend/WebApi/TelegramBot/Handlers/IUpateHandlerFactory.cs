using Telegram.Bot.Types.Enums;

namespace TelegramBot.Handlers;

public interface IUpdateHandlerFactory
{
    IUpdateHandler Create(UpdateType type);
}