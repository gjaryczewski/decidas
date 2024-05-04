using Decidas.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decidas.Areas.Structure.Models;

public class AssignmentDbConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("Assignments", schema: "Structure");

        builder.HasKey(a => new { a.GroupId, a.KeeperId });

        builder.Property(a => a.GroupId)
            .ValueGeneratedNever()
            .HasConversion(gi => gi.Value, v => new GroupId(v));

        builder.Property(a => a.KeeperId)
            .ValueGeneratedNever()
            .HasConversion(ki => ki.Value, v => new KeeperId(v));

        builder.Property(a => a.AssignDate)
            .HasColumnType("date")
            .IsRequired();
    }
}
