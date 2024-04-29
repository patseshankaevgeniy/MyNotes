using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class UserNoteConfiguration : IEntityTypeConfiguration<UserNote>
{
    public void Configure(EntityTypeBuilder<UserNote> builder)
    {
        builder.ToTable("UserNotes", "dbo");
        builder.HasKey(n => n.Id);

        builder
            .HasOne(n => n.User);
    }
}
