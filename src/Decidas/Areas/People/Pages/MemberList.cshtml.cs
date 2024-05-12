using Decidas.Areas.People.Models;
using Decidas.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Decidas.Areas.People.Pages;

public class MemberListModel(GetMemberListQuery _query) : PaginatedPageModel
{
    public List<MemberType> Items { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int page = 1, int perPage = 30, CancellationToken cancel = default)
    {
        var request = new GetMemberListRequest(page, perPage);

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
