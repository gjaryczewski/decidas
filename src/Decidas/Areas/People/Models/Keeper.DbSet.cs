using Decidas.Areas.People.Models;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Core;

public partial class ApplicationDb
{
    public DbSet<Keeper> Keepers { get; set; }
}
