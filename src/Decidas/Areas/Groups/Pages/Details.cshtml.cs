using Decidas.Areas.Groups.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Groups.Pages;

public class GetGroupModel(GetGroupQuery _query) : PageModel
{
    public GroupDetails GroupDetails { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id, CancellationToken cancel)
    {
        if (id is null)
        {
            return NotFound();
        }

        var request = new GetGroupRequest((Guid)id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response == null)
        {
            return NotFound();
        }

        GroupDetails = (GroupDetails)response;

        return Page();
    }
}
