using Application.Common.Interfaces;
using Application.Common.Models;
using Application.TelegramBot.Common.Services.Interfaces;
using System;

namespace Application.TelegramBot.Common.Services;

public sealed class TelegramBotMessagesProvider : ITelegramBotMessagesProvider
{
    private readonly IConstantsService _constantsService;
    private readonly ICurrentUserService _currentUserService;

    public TelegramBotMessagesProvider(
        IConstantsService constantsService,
        ICurrentUserService currentUserService)
    {
        _constantsService = constantsService;
        _currentUserService = currentUserService;
    }

    public string Build(Func<TelegramBotMessages, string> selector, params object[] args)
    {
        return string.Format(Get(selector), args);
    }

    public string Get(Func<TelegramBotMessages, string> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        var languageId = _currentUserService.User?.LanguageId ?? _constantsService.DefaultLanguageId;
        return Get(languageId, selector);
    }

    public string Get(Guid languageId, Func<TelegramBotMessages, string> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        var telegramBotMessages = new TelegramBotMessages();

        return selector.Invoke(telegramBotMessages);
    }
}