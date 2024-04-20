using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Decidas;
using Decidas.Models;

namespace Decidas.Pages_Gatherings
{
    public class CreateModel : PageModel
    {
        private readonly Decidas.MainDbContext _context;

        public CreateModel(Decidas.MainDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Gathering Gathering { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Gatherings.Add(Gathering);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
