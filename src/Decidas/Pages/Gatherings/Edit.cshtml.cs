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

namespace Decidas.Pages.Gatherings
{
    public class EditModel : PageModel
    {
        private readonly Decidas.MainDbContext _context;

        public EditModel(Decidas.MainDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Gathering Gathering { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering =  await _context.Gatherings.FirstOrDefaultAsync(m => m.Id == id);
            if (gathering == null)
            {
                return NotFound();
            }
            Gathering = gathering;
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

            _context.Attach(Gathering).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatheringExists(Gathering.Id))
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

        private bool GatheringExists(Guid id)
        {
            return _context.Gatherings.Any(e => e.Id == id);
        }
    }
}
