using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.TelegramBot.TextMessages.TextDecompositions.Models;

public sealed class TextDecomposition
{
    public IReadOnlyList<TextItem> Items { get; set; } = new List<TextItem>();

    public int GetCountOf(TextItemType type) => Items.Count(x => x.Type == type);

    public List<T> GetValues<T>(TextItemType type)
        where T : notnull
    {
        return Items
            .Where(item => item.Type == type)
            .Select(item => (T)Convert.ChangeType(item.Value, typeof(T)))
            .ToList();
    }
}