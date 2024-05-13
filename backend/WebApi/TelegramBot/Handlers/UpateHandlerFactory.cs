using Application.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Handlers;

public sealed class UpdateHandlerFactory : IUpdateHandlerFactory
{
    private readonly IEnumerable<IUpdateHandler> _handlers;

    public UpdateHandlerFactory(IEnumerable<IUpdateHandler> handlers)
    {
        _handlers = handlers;
    }

    public IUpdateHandler Create(UpdateType type)
    {
        if (!_handlers.Any(t => t.UpdateType == UpdateType.Unknown))
        {
            throw new WrongSetupException("Please register at least Unknown handler.");
        }

        var handler = _handlers.FirstOrDefault(t => t.UpdateType == type);

        if (handler == null)
        {
            handler = _handlers.First(t => t.UpdateType == UpdateType.Unknown);
        }

        return handler;
    }
}