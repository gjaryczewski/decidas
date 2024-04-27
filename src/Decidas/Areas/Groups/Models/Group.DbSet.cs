
using Decidas.Areas.Groups.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decidas.Core;

public partial class ApplicationDb
{
    public DbSet<Group> Groups { get; set; }
}
