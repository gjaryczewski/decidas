using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.People.Pages;

public class KeeperDetailsModel(GetKeeperDetailsCommand _query) : PageModel
{
    public KeeperDetails? Keeper { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id, CancellationToken cancel)
    {
        if (id is null)
        {
            return NotFound();
        }

        var request = new KeeperDetailsRequest((Guid)id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response == null)
        {
            return NotFound();
        }

        Keeper = response;

        return Page();
    }
}
