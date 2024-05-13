namespace Application.TelegramBot.TextMessages.TextDecompositions.Models;

public sealed class TextItem
{
    public TextItemType Type { get; set; }
    public string Value { get; init; } = default!;
    public int OrderNumber { get; init; }

    public TextItem(string value, TextItemType type, int orderNumber)
    {
        Value = value;
        Type = type;
        OrderNumber = orderNumber;
    }
}