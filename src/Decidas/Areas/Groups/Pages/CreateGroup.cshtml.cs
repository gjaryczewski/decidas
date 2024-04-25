using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Groups.Pages;

public class CreateGroupModel : PageModel
{
    private readonly ILogger<CreateGroupModel> _logger;

    public CreateGroupModel(ILogger<CreateGroupModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
