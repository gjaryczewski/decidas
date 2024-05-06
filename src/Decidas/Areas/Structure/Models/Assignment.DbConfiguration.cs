using Decidas.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decidas.Areas.Structure.Models;

public class AssignmentDbConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasConversion(i => i.Value, v => new AssignmentId(v));

        builder.HasAlternateKey(a => new { a.GroupId, a.KeeperId });

        builder.Property(a => a.GroupId)
            .ValueGeneratedNever()
            .HasConversion(gi => gi.Value, v => new GroupId(v));

        builder.HasIndex(a => a.GroupId);

        builder.Property(a => a.KeeperId)
            .ValueGeneratedNever()
            .HasConversion(ki => ki.Value, v => new KeeperId(v));

        builder.HasIndex(a => a.KeeperId);

        builder.Property(a => a.AssignDate)
            .HasColumnType("date")
            .IsRequired();
    }
}
