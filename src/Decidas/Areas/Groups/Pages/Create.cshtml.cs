using Decidas.Areas.Groups.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Groups.Pages;

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
        var response = await _command.ProcessAsync(request, cancel);

        return RedirectToPage("Details", new { response.Id } );
    }
}
