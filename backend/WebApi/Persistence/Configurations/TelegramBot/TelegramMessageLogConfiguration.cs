using Domain.Entities.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.TelegramBot;

public sealed class TelegramMessageLogConfiguration : IEntityTypeConfiguration<TelegramMessageLog>
{
    public void Configure(EntityTypeBuilder<TelegramMessageLog> builder)
    {
        builder.ToTable("TelegramMessageLogs", "providers");
        builder.HasKey(t => new { t.TelegramUserId, t.TelegramMessageId, t.TelegramChatId });
    }
}