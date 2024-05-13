using Domain.Entities.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;

namespace Persistence.Configurations.TelegramBot;

public sealed class TelegramBotConfigurationConfiguration : IEntityTypeConfiguration<TelegramBotConfiguration>
{
    public void Configure(EntityTypeBuilder<TelegramBotConfiguration> builder)
    {
        builder.ToTable("TelegramBotConfigurations", "providers");
        builder.HasKey(t => t.Id);


        builder
            .HasOne(t => t.TelegramUser)
            .WithOne(t => t.TelegramBotConfiguration)
            .HasForeignKey<TelegramBotConfiguration>(t => t.TelegramUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}