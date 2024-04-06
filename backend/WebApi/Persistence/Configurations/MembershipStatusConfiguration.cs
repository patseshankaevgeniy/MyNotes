using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class MembershipStatusConfiguration : IEntityTypeConfiguration<MembershipStatus>
{
    public void Configure(EntityTypeBuilder<MembershipStatus> builder)
    {
        builder.ToTable("MembershipStatuses", "dbo");
        builder.HasKey(t => t.Id);

        builder
            .HasOne(t => t.User)
            .WithOne(t => t.MembershipStatus)
            .HasForeignKey<MembershipStatus>(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}