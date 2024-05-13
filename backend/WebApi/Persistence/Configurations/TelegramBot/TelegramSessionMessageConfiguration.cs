using Domain.Entities.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.TelegramBot;

public sealed class TelegramSessionMessageConfiguration : IEntityTypeConfiguration<TelegramSessionMessage>
{
    public void Configure(EntityTypeBuilder<TelegramSessionMessage> builder)
    {
        builder.ToTable("TelegramSessionMessages", "providers");
        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Type)
            .HasConversion<string>();

        builder
            .Property(t => t.State)
            .HasConversion<string>();

        builder
            .HasOne(t => t.Session)
            .WithMany(t => t.Messages)
            .HasForeignKey(t => t.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}