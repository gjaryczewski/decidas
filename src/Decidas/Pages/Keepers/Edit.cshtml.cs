using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Decidas;
using Decidas.Models;

namespace Decidas.Pages.Keepers
{
    public class EditModel : PageModel
    {
        private readonly Decidas.MainDbContext _context;

        public EditModel(Decidas.MainDbContext context)
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

            var keeper =  await _context.Keepers.FirstOrDefaultAsync(m => m.Id == id);
            if (keeper == null)
            {
                return NotFound();
            }
            Keeper = keeper;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Keeper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeeperExists(Keeper.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool KeeperExists(Guid id)
        {
            return _context.Keepers.Any(e => e.Id == id);
        }
    }
}
