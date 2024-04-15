using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Decidas;
using Decidas.Models;

namespace Decidas.Pages.Gatherings
{
    public class IndexModel : PageModel
    {
        private readonly Decidas.MainDbContext _context;

        public IndexModel(Decidas.MainDbContext context)
        {
            _context = context;
        }

        public IList<Gathering> Gathering { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Gathering = await _context.Gatherings.ToListAsync();
        }
    }
}
