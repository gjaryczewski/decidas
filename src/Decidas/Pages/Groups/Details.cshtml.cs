using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Decidas;
using Decidas.Models;

namespace Decidas.Pages.Groups
{
    public class DetailsModel : PageModel
    {
        private readonly Decidas.MainDbContext _context;

        public DetailsModel(Decidas.MainDbContext context)
        {
            _context = context;
        }

        public Group Group { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FirstOrDefaultAsync(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            else
            {
                Group = group;
            }
            return Page();
        }
    }
}
