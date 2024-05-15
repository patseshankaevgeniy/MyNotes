namespace TelegramBot.Options;

public sealed class TelegraBotOptions
{
    public const string SectionName = "TelegramBot";

    public string LinkTemplate { get; init; } = default!;
    public string Token { get; init; } = default!;
}