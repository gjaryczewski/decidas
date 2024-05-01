using Decidas.Areas.People.Features;
using Decidas.Areas.People.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.People.Pages;

public class KeeperListModel(GetKeeperListQuery _query) : PageModel
{
    public List<KeeperType> Items { get; set; } = [];

    public int Count { get; set; }

    public int PageNumber { get; set; }

    public int PerPage { get; set; }

    public async Task<IActionResult> OnGetAsync(int page = 1, int perPage = 30, CancellationToken cancel = default)
    {
        var request = new GetKeeperListRequest(page, perPage);

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
