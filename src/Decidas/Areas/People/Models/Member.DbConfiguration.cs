using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decidas.Areas.People.Models;

public class MemberDbConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(member => member.Id);

        builder.Property(member => member.Id)
            .HasConversion(id => id.Value, value => new MemberId(value));

        builder.Property(member => member.Name)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(member => member.Login)
            .HasColumnType("nvarchar(50)")
            .IsRequired()
            .HasConversion(login => login.Value, value => new Login(value));

        builder.Property(member => member.Email)
            .HasColumnType("nvarchar(200)")
            .IsRequired()
            .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(member => member.PasswordHash)
            .HasColumnType("nvarchar(40)")
            .IsRequired()
            .HasConversion(password => password.Value, value => new PasswordHash(value));

        builder.Property(member => member.RegisterDate)
            .HasColumnType("date")
            .IsRequired();
    }
}
