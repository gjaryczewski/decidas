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
}