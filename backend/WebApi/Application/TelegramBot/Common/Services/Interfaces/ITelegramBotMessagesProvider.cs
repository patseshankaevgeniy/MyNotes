using Application.Common.Models;
using System;

namespace Application.TelegramBot.Common.Services.Interfaces;

public interface ITelegramBotMessagesProvider
{
    string Get(Func<TelegramBotMessages, string> selector);
    string Get(Guid languageId, Func<TelegramBotMessages, string> selector);

    string Build(Func<TelegramBotMessages, string> selector, params object[] args);
}