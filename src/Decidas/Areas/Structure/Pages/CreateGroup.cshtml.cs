using Decidas.Areas.Structure.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Structure.Pages;

public class CreateGroupModel(CreateGroupCommand _command) : PageModel
{
    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public DateTime StartDate { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancel)
    {
        var request = new CreateGroupRequest(Name, StartDate);
        var response = await _command.ExecuteAsync(request, cancel);

        return RedirectToPage("Details", new { id = response.Value } );
    }
}
