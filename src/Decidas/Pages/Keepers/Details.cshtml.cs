using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Decidas;
using Decidas.Models;

namespace Decidas.Pages.Keepers
{
    public class DetailsModel : PageModel
    {
        private readonly Decidas.MainDbContext _context;

        public DetailsModel(Decidas.MainDbContext context)
        {
            _context = context;
        }

        public Keeper Keeper { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keeper = await _context.Keepers.FirstOrDefaultAsync(m => m.Id == id);
            if (keeper == null)
            {
                return NotFound();
            }
            else
            {
                Keeper = keeper;
            }
            return Page();
        }
    }
}
