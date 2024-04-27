using Decidas.Areas.Groups.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Areas.Groups.Pages;

public class CreateGroupModel : PageModel
{
    private readonly ILogger<CreateGroupModel> _logger;
    private readonly CreateGroupCommand _command;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public DateTime StartDate { get; set; }

    public CreateGroupModel(ILogger<CreateGroupModel> logger, CreateGroupCommand command)
    {
        _logger = logger;
        _command = command;
    }

    public void OnGet()
    {
        _logger.LogTrace($"Rendering page {nameof(CreateGroupModel)}");
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancel)
    {
        _logger.LogTrace($"Processing OnPost of {nameof(CreateGroupModel)}");

        var request = new CreateGroupRequest(Name, StartDate);
        var response = await _command.ProcessAsync(request, cancel);

        return RedirectToPage("./Index");
    }
}
