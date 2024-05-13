using Domain.Entities.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.TelegramBot;

public sealed class TelegramAuthCodeConfiguration : IEntityTypeConfiguration<TelegramAuthCode>
{
    public void Configure(EntityTypeBuilder<TelegramAuthCode> builder)
    {
        builder.ToTable("TelegramAuthCodes", "providers");
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.ShortCode).IsUnique();
    }
}