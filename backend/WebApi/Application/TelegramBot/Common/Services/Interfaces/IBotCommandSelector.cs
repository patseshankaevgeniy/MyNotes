using Application.TelegramBot.TextMessages.Models;
using Application.TelegramBot.TextMessages.TextDecompositions.Models;
using System.Threading.Tasks;

namespace Application.TelegramBot.Common.Services.Interfaces;

public interface IBotCommandSelector
{
    Task<SelectionResult> AnalyzeAsync(TextDecomposition decomposition);
}