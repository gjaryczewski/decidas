using Decidas.Areas.People.Features;
using Decidas.Areas.Structure.Features;
using Decidas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Structure.Pages;

public class AssignKeeperModel(GetKeeperListQuery _keeperQuery, AssignKeeperCommand _assignCommand) : PageModel
{
    [BindProperty]
    public Guid GroupId { get; set; } = default!;

    [BindProperty]
    public string GroupName { get; set; } = default!;

    [BindProperty]
    public Guid KeeperId { get; set; } = default!;

    [BindProperty]
    public DateOnly AssignDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [BindProperty]
    public List<KeeperName> Keepers { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(Guid? id, CancellationToken cancel)
    {
        if (id is null)
        {
            return NotFound();
        }

        var request = new GetKeeperListRequest(1, 100);

        var response = await _keeperQuery.ExecuteAsync(request, cancel);

        if (response == null)
        {
            return NotFound();
        }

        response.Items.ForEach(keeper => Keepers.Add(new KeeperName(keeper.Id, keeper.Name)));

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancel)
    {
        var request = new AssignKeeperRequest(GroupId, KeeperId, AssignDate.ToDateTime());

        var response = await _assignCommand.ExecuteAsync(request, cancel);

        return RedirectToPage("GroupDetails", new { id = GroupId } );
    }

    public record KeeperName(Guid Id, string Name);
}
