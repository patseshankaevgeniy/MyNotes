using Application.TelegramBot.Common.Services.Interfaces;
using Application.TelegramBot.TextMessages.Models;
using Application.TelegramBot.TextMessages.TextDecompositions.Models;
using System.Threading.Tasks;

namespace Application.TelegramBot.UserNotes;

public sealed class AddUserNoteBotCommandSelector : IBotCommandSelector
{
    public Task<SelectionResult> AnalyzeAsync(TextDecomposition decomposition)
    {
        if (decomposition.GetCountOf(TextItemType.Word) < 1 && decomposition.GetCountOf(TextItemType.Number) < 1)
        {
            return Task.FromResult(SelectionResult.CreateDoesntSuits());
        }

        if (decomposition.GetCountOf(TextItemType.Number) > 1 || decomposition.GetCountOf(TextItemType.Word) > 1)
        {
            var result = new SelectionResult
            {
                Suits = true,
                CompatibilityInPercent = 90,
                BotCommand = new AddUserNoteBotCommand
                {
                    Text = decomposition.GetValues<string>(TextItemType.Word).ToString(),
                }
            };

            return Task.FromResult(result);
        }
        else
        {
            return Task.FromResult(new SelectionResult
            {
                Suits = true,
                CompatibilityInPercent = 70,
                BotCommand = new AddUserNoteBotCommand
                {
                    Text = decomposition.GetValues<string>(TextItemType.Word).ToString(),
                }
            });
        }
    }
}
