using Application.TelegramBot.Common.Models;
using Application.TelegramBot.Common.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.MessageSender;

public sealed class TelegramMessageSender : ITelegramMessageSender
{
    private readonly ITelegramBotClient _botClient;

    public TelegramMessageSender(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task<BotMessage> SendTextMessageAsync(long chatId, string text, IEnumerable<MessageButtonModel>? buttons = default)
    {
        InlineKeyboardMarkup replyMarkup = null;

        if (buttons != null && buttons.Any())
        {
            replyMarkup = new InlineKeyboardMarkup(buttons
                .GroupBy(button => button.Row)
                .OrderBy(group => group.Key)
                .Select(group => group
                    .OrderBy(button => button.Column)
                    .Select(button => InlineKeyboardButton.WithCallbackData(text: button.Text, callbackData: button.CalbackData))));
        }

        var message = await _botClient.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup);

        return new BotMessage
        {
            Id = message.MessageId,
            Date = message.Date,
            Text = message.Text
        };
    }

    public async Task<BotMessage> EditTextMessageAsync(long chatId, int messageId, string text, IEnumerable<MessageButtonModel>? buttons = default)
    {
        InlineKeyboardMarkup replyMarkup = null;

        if (buttons != null && buttons.Any())
        {
            replyMarkup = new InlineKeyboardMarkup(buttons
                .GroupBy(button => button.Row)
                .OrderBy(group => group.Key)
                .Select(group => group
                    .OrderBy(button => button.Column)
                    .Select(button => InlineKeyboardButton.WithCallbackData(text: button.Text, callbackData: button.CalbackData))));
        }

        var message = await _botClient.EditMessageTextAsync(chatId, messageId, text, replyMarkup: replyMarkup);

        return new BotMessage
        {
            Id = message.MessageId,
            Date = message.Date,
            Text = message.Text
        };
    }

    public async Task<BotMessage> SendTextMessageWithUrlButtonAsync(long chatId, string text, string url)
    {
        var replyMarkup = new InlineKeyboardMarkup(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithUrl(text, url)
        });

        var message = await _botClient.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup);

        return new BotMessage
        {
            Id = message.MessageId,
            Date = message.Date,
            Text = message.Text
        };
    }

    public async Task DeleteTextMessageAsync(long chatId, int messageId)
    {
        await _botClient.DeleteMessageAsync(chatId, messageId);
    }
}