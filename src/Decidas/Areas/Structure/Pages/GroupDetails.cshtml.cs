using Decidas.Areas.Structure.Features;
using Decidas.Areas.Structure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Structure.Pages;

public class GroupDetailsModel(GetGroupQuery _query) : PageModel
{
    public GroupType Group { get; set; } = default!;

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

        Group = response;

        return Page();
    }
}
