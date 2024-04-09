using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Web.Pages.Groups;

public class IndexModel : PageModel
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IDispatcher dispatcher, ILogger<IndexModel> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public void OnGet()
    {

    }
}