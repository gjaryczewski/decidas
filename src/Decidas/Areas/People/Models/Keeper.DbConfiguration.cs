using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decidas.Areas.People.Models;

public class KeeperDbConfiguration : IEntityTypeConfiguration<Keeper>
{
    public void Configure(EntityTypeBuilder<Keeper> builder)
    {
        builder.HasKey(keeper => keeper.Id);

        builder.Property(keeper => keeper.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new KeeperId(value));

        builder.Property(keeper => keeper.MemberId)
            .ValueGeneratedNever()
            .HasConversion(memberId => memberId.Value, value => new MemberId(value));

        builder.HasOne(e => e.Member)
            .WithOne(e => e.Keeper)
            .HasForeignKey<Keeper>(e => e.MemberId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(keeper => keeper.DesignateDate)
            .HasColumnType("date")
            .IsRequired();
    }
}
