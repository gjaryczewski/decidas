
using Decidas.Areas.Members.Models;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Core;

public partial class ApplicationDb
{
    public DbSet<Member> Members { get; set; }
}
