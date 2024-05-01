using Decidas.Areas.People.Features;
using Decidas.Areas.People.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.People.Pages;

public class KeeperDetailsModel(GetKeeperQuery _query) : PageModel
{
    public KeeperType? Keeper { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id, CancellationToken cancel)
    {
        if (id is null)
        {
            return NotFound();
        }

        var request = new GetKeeperRequest((Guid)id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response == null)
        {
            return NotFound();
        }

        Keeper = response;

        return Page();
    }
}
