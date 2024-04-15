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
    public class DeleteModel : PageModel
    {
        private readonly Decidas.MainDbContext _context;

        public DeleteModel(Decidas.MainDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keeper = await _context.Keepers.FindAsync(id);
            if (keeper != null)
            {
                Keeper = keeper;
                _context.Keepers.Remove(Keeper);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
