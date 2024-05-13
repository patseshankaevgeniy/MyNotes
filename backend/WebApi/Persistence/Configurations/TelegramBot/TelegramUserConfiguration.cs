using Domain.Entities.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expenses.Persistence.Configurations.TelegramBot;

public sealed class TelegramUserConfiguration : IEntityTypeConfiguration<TelegramUser>
{
    public void Configure(EntityTypeBuilder<TelegramUser> builder)
    {
        builder.ToTable("TelegramUsers", "providers");
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => new { t.UserId, TelegramUserId = t.OriginalTelegramUserId, TelegramBotId = t.TelegramBotToken }).IsUnique();
    }
}
