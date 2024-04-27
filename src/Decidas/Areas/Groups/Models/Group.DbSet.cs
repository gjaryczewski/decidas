
using Decidas.Areas.Groups.Models;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Core;

public partial class ApplicationDb
{
    public DbSet<Group> Groups { get; set; }
}
