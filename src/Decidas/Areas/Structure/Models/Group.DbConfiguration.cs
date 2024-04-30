using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decidas.Areas.Structure.Models;

public class GroupDbConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(group => group.Id);

        builder.Property(group => group.Id)
            .HasConversion(id => id.Value, value => new GroupId(value));

        builder.Property(group => group.Name)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(group => group.StartDate)
            .HasColumnType("date")
            .IsRequired()
            .HasConversion(startDate => startDate.Value, value => new GroupStartDate(value));
    }
}
