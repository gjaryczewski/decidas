using Decidas.Areas.Structure.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Structure.Pages;

public class GetGroupDetailsModel(GetGroupDetailsQuery _query) : PageModel
{
    public GroupDetails GroupDetails { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id, CancellationToken cancel)
    {
        if (id is null)
        {
            return NotFound();
        }

        var request = new GetGroupDetailsRequest((Guid)id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response == null)
        {
            return NotFound();
        }

        GroupDetails = (GroupDetails)response;

        return Page();
    }
}
