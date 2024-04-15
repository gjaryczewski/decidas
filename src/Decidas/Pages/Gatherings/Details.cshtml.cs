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
    public class DetailsModel : PageModel
    {
        private readonly Decidas.MainDbContext _context;

        public DetailsModel(Decidas.MainDbContext context)
        {
            _context = context;
        }

        public Gathering Gathering { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering = await _context.Gatherings.FirstOrDefaultAsync(m => m.Id == id);
            if (gathering == null)
            {
                return NotFound();
            }
            else
            {
                Gathering = gathering;
            }
            return Page();
        }
    }
}
