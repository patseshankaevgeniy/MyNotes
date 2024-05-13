using Domain.Entities.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.TelegramBot;

public sealed class TelegramSessionConfiguration : IEntityTypeConfiguration<TelegramSession>
{
    public void Configure(EntityTypeBuilder<TelegramSession> builder)
    {
        builder.ToTable("TelegramSessions", "providers");
        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Type)
            .HasConversion<string>();

        builder
            .Property(t => t.Status)
            .HasConversion<string>();
    }
}