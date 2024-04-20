using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Pages
{
    public class ForbiddenModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Forbidden page.";
        }
    }
}
