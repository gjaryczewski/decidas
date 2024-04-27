using Decidas.Areas.Groups.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Groups.Pages;

public class CreateGroupModel(ILogger<CreateGroupModel> logger, CreateGroupCommand command) : PageModel
{
    private readonly ILogger<CreateGroupModel> _logger = logger;
    private readonly CreateGroupCommand _command = command;

    [BindProperty]
    public CreateGroupRequest FormModel { get; set; }

    public void OnGet()
    {
    }

    public async Task<RedirectResult> OnPostAsync()
    {
        var response = await _command.ProcessAsync(FormModel);

        return Redirect("/");
    }
}
