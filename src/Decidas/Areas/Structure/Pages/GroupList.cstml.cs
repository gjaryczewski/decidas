using Decidas.Areas.Structure;
using Decidas.Areas.Structure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Structure.Pages;

public class GroupListModel(GetGroupListQuery _query) : PageModel
{
    public List<GroupType> Items { get; set; } = [];

    public int Count { get; set; }

    public int PageNumber { get; set; }

    public int PerPage { get; set; }

    public async Task<IActionResult> OnGetAsync(int page = 1, int perPage = 30, CancellationToken cancel = default)
    {
        var request = new GetGroupListRequest(page, perPage);

        var response = await _query.ExecuteAsync(request, cancel);

        if (response is null)
        {
            return NotFound();
        }

        Items = response.Items;
        Count = response.Count;
        PageNumber = response.Page;
        PerPage = response.PerPage;

        return Page();
    }
}
