using Decodas.Domain.Groups;
using Microsoft.EntityFrameworkCore;

namespace Decodas.Core;

public partial class MainDb : DbContext
{
    public DbSet<Group> Groups { get; set; } = new();
}

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder
            .Property(b => b.Url)
            .IsRequired();
    }
}