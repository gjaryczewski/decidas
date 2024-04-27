using Microsoft.EntityFrameworkCore;

namespace Decidas.Core;

public partial class ApplicationDb(DbContextOptions<ApplicationDb> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDb).Assembly);
    }
}