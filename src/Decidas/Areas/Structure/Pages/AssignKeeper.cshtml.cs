using Decidas.Areas.Structure.Clients;
using Decidas.Areas.Structure.Features;
using Decidas.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Structure.Pages;

public class AssignKeeperModel(PeopleClient _peopleClient, GetGroupQuery _getGroup, AssignKeeperCommand _assignKeeper) : PageModel
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

    public async Task<IActionResult> OnGetAsync(Guid? groupId, CancellationToken cancel)
    {
        if (groupId is null)
        {
            return NotFound();
        }

        var request = new GetGroupRequest((Guid)groupId);

        var group = await _getGroup.ExecuteAsync(request, cancel);
        if (groupId is null)
        {
            return NotFound();
        }

        GroupId = group!.Id;
        GroupName = group!.Name;

        var response = await _peopleClient.GetKeeperList(cancel);
        if (response == null)
        {
            return NotFound();
        }

        response.ForEach(keeper => Keepers.Add(new KeeperName(keeper.Id, keeper.Name)));

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancel)
    {
        var request = new AssignKeeperRequest(GroupId, KeeperId, AssignDate.ToDateTime());

        await _assignKeeper.ExecuteAsync(request, cancel);

        return RedirectToPage("GroupDetails", new { id = GroupId } );
    }

    public record KeeperName(Guid Id, string Name);
}
