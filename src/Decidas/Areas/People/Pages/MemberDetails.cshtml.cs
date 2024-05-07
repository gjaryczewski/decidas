using Decidas.Areas.People;
using Decidas.Areas.People.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.People.Pages;

public class MemberDetailsModel(GetMemberQuery _query) : PageModel
{
    public MemberType? Member { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id, CancellationToken cancel)
    {
        if (id is null)
        {
            return NotFound();
        }

        var request = new GetMemberRequest((Guid)id);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response == null)
        {
            return NotFound();
        }

        Member = response;

        return Page();
    }
}
