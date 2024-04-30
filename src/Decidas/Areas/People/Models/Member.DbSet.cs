
using Decidas.Areas.People.Models;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Core;

public partial class ApplicationDb
{
    public DbSet<Member> Members { get; set; }
}
