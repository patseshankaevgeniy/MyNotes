using Application.TelegramBot.Auth.Commands;
using Application.TelegramBot.Auth.Models;
using Application.TelegramBot.Common.Services.Interfaces;
using Application.TelegramBot.TextMessages;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Handlers;

public sealed class MessageUpdateHandler : IUpdateHandler
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly ITelegramBotMessagesProvider _messages;
    private readonly ITelegramMessageSender _telegramMessageSender;

    public UpdateType UpdateType => UpdateType.Message;

    public MessageUpdateHandler(
        IMediator mediator,
        ILogger<MessageUpdateHandler> logger,
        ITelegramBotMessagesProvider messages,
        ITelegramMessageSender telegramMessageSender)
    {
        _logger = logger;
        _mediator = mediator;
        _messages = messages;
        _telegramMessageSender = telegramMessageSender;
    }

    public async Task HandleAsync(Update update, CancellationToken token)
    {
        var user = update.Message!.From!;
        var chatId = update.Message.Chat.Id;
        var userMessage = update.Message.Text;

        var authResult = await _mediator.Send(new AuthenticateTelegramBotCommand
        {
            ChatId = chatId,
            UserId = user.Id,
            UserName = user.Username,
            UserFirstName = user.FirstName,
            UserLastName = user.LastName,
            LanguageCode = user.LanguageCode,
            UserMessage = userMessage
        });

        if (authResult != TelegramAuthResult.Succeeded)
        {
            await HandleAuthResultAsync(authResult, chatId);
            return;
        }

        try
        {
            await _mediator.Send(new HandlerTextMessageUpdateCommand(userMessage), token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await _telegramMessageSender.SendTextMessageAsync(chatId, _messages.Get(m => m.ServerError));
        }

    }

    private async Task HandleAuthResultAsync(TelegramAuthResult authResult, long chantId)
    {
        var message = authResult switch
        {
            TelegramAuthResult.FirstLogin => _messages.Get(m => m.WelcomeNewTelegramUser),
            TelegramAuthResult.UserNotFound => _messages.Get(m => m.UserNotAuthenticated),
            TelegramAuthResult.CodeNotValid => _messages.Get(m => m.WrongAuthCode),
            _ => _messages.Get(m => m.UserNotAuthenticated)
        };

        await _telegramMessageSender.SendTextMessageAsync(chantId, message);
    }
}