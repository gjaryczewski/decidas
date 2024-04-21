using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Decidas.ViewComponents;

public class KeepersViewComponent : ViewComponent
{
    private MainDbContext _dbContext;

    public KeepersViewComponent(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid groupId)
    {
        var accounts = await _dbContext.Keepers
            .Where(k => k.Group.Id == groupId)
            .Join(_dbContext.Accounts,
                keeper => keeper.Account.Id, account => account.Id,
                (keeper, account) => account)
            .ToListAsync();

        return View(accounts);
    }
}