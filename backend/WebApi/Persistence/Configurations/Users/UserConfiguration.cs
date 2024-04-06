using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Users;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "dbo");
        builder.HasKey(t => t.Id);

        builder
            .HasOne(t => t.Group)
            .WithMany(t => t.Users)
            .HasForeignKey(t => t.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}