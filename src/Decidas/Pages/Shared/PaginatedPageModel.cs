using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Pages.Shared;

public class PaginatedPageModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    public int Count { get; set; }

    public int PerPage { get; set; } = 30;

    public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PerPage));

    public bool ShowFirstPage => PageNumber != 1;

    public int PreviousPage => PageNumber - 1;

    public bool ShowPreviousPage => PageNumber > 1;

    public int NextPage => PageNumber + 1;

    public bool ShowNextPage => PageNumber < TotalPages;

    public bool ShowLastPage => PageNumber != TotalPages;

}
