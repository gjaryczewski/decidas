using Decidas.Areas.Structure.Models;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Core;

public partial class ApplicationDb
{
    public DbSet<Assignment> Assignments { get; set; } = default!;
}
