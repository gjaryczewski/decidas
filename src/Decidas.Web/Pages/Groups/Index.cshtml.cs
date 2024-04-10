using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Web.Pages.Groups;

public class IndexModel : PageModel
{
    private readonly ICommandDispatcher _dispatcher;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ICommandDispatcher dispatcher, ILogger<IndexModel> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public void OnGet()
    {

    }
}