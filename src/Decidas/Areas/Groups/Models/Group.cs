using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decidas.Areas.Groups.Models;

internal readonly record struct GroupId(Guid Value);

internal sealed class Group
{
    public GroupId Id { get; private set; }

    public required string Name { get; set; }

    public DateOnly StartDate { get; private set; }

    public Group() {}

    public static Group Create(string name, DateOnly startDate)
    {
        return new()
        {
            Id = new(Guid.NewGuid()),
            Name = name,
            StartDate = startDate
        };
    }
}

internal class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Groups");

        builder.HasKey(group => group.Id);

        builder.Property(group => group.Id).HasConversion(id => id.Value, value => new GroupId(value));

        builder.Property(group => group.Name).HasColumnType("nvarchar(100)").IsRequired();

        builder.Property(group => group.StartDate).HasColumnType("date").IsRequired();
    }
}