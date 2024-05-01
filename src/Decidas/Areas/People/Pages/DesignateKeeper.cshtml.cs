using Decidas.Areas.People.Features;
using Decidas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.People.Pages;

public class DesignateKeeperModel(GetMemberQuery _query, DesignateKeeperCommand _command) : PageModel
{
    [BindProperty]
    public Guid MemberId { get; set; } = Guid.Empty;
    
    [BindProperty]
    public string Name { get; set; } = string.Empty;
    
    [BindProperty]
    public DateOnly DesignateDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

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

        MemberId = response.Id;
        Name = response.Name;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancel)
    {
        var request = new DesignateKeeperRequest(MemberId, DesignateDate.ToDateTime());

        var response = await _command.ExecuteAsync(request, cancel);

        return RedirectToPage("KeeperDetails", new { id = response.Value } );
    }
}
