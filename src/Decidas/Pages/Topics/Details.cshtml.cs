using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Decidas;
using Decidas.Models;

namespace Decidas.Pages.Topics
{
    public class DetailsModel : PageModel
    {
        private readonly Decidas.MainDbContext _context;

        public DetailsModel(Decidas.MainDbContext context)
        {
            _context = context;
        }

        public Topic Topic { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }
            else
            {
                Topic = topic;
            }
            return Page();
        }
    }
}
