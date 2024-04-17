using Decidas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Decidas;

public class MainDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public DbSet<Gathering> Gatherings { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Keeper> Keepers { get; set; }

    public DbSet<Member> Members { get; set; }

    public DbSet<Topic> Topics { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) {}

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     builder.Entity<Account>()
    //         .Property(p => p.Id)
    //         .HasColumnType("uniqueidentifier")
    //         .ValueGeneratedNever();
    //     builder.Entity<Account>()
    //         .Property(p => p.Login)
    //         .HasColumnType("varchar")
    //         .HasMaxLength(40)
    //         .IsRequired();
    //     builder.Entity<Account>()
    //         .Property(p => p.Email)
    //         .HasColumnType("varchar")
    //         .HasMaxLength(320)
    //         .IsRequired();
    //     builder.Entity<Account>()
    //         .Property(p => p.Name)
    //         .HasColumnType("varchar")
    //         .HasMaxLength(320)
    //         .IsRequired();
    //     builder.Entity<Account>()
    //         .Property(p => p.Password)
    //         .HasColumnType("char")
    //         .HasMaxLength(40)
    //         .IsRequired();
    //     builder.Entity<Account>()
    //         .Property(p => p.RegisterTime)
    //         .HasColumnType("datetime")
    //         .IsRequired();
    // }
}