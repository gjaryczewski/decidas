using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Decidas.Web.Pages.Groups;

public class CreateModel : PageModel
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(IDispatcher dispatcher, ILogger<CreateModel> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public void OnGet()
    {

    }

    [BindProperty]
    public CreateGroupRequest Request { get; set; }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null, CancellationToken cancel = default)
    {
        returnUrl ??= Url.Content("~/");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var command = new CreateGroupCommand(
            Request.Name,
            DateOnly.FromDateTime(Request.StartDate.Date));

        var result = await _dispatcher.Send(command, cancel);

        return LocalRedirect(returnUrl);
    }
}

public record CreateGroupRequest(string Name, DateTime StartDate);

public interface ICommand;

public record CreateGroupCommand(string Name , DateOnly StartDate) : ICommand;

public record Result(bool IsSuccess); 

public interface IDispatcher
{
    Task<Result> Send(ICommand command, CancellationToken cancel);
}

public class Dispatcher : IDispatcher
{
    private readonly ILogger<Dispatcher> _logger;

    public Dispatcher(ILogger<Dispatcher> logger)
    {
        _logger = logger;
    }

    public Task<Result> Send(ICommand command, CancellationToken cancel)
    {
        var result = new Result(true);

        _logger.LogInformation($"The group has been created.");

        return Task.FromResult(result);
    }
}