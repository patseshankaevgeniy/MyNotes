using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Users;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "dbo");
        builder.HasKey(u => u.Id);

        builder
            .HasOne(u => u.Group)
            .WithMany(u => u.Users)
            .HasForeignKey(u => u.GroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Notes);
    }
}