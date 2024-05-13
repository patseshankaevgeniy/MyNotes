using Application.TelegramBot.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.TelegramBot.Common.Services.Interfaces;

public interface ITelegramMessageSender
{
    Task<BotMessage> SendTextMessageAsync(long chatId, string text, IEnumerable<MessageButtonModel>? buttons = default);
    Task<BotMessage> EditTextMessageAsync(long chatId, int messageId, string text, IEnumerable<MessageButtonModel>? buttons = default);
    Task<BotMessage> SendTextMessageWithUrlButtonAsync(long chatId, string text, string url);
    Task DeleteTextMessageAsync(long chatId, int messageId);
}