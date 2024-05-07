using Decidas.Areas.Structure.Models;
using Decidas.Core;
using Microsoft.EntityFrameworkCore;

namespace Decidas.Areas.Structure.Policies;

public class KeepingPolicy(ApplicationDb _db)
{
    public const int MaxAssignmentsPerKeeper = 2;

    public async Task EvaluateAsync(KeeperId keeperId)
    {
        var assigned = await _db.Assignments.Where(a => a.KeeperId == keeperId).CountAsync();
        if (assigned >= MaxAssignmentsPerKeeper)
        {
            throw new TooManyAssignmentsPerKeeper(keeperId);
        }
    }
}

#region Errors

public class TooManyAssignmentsPerKeeper : DomainError
{
    public TooManyAssignmentsPerKeeper(KeeperId keeperId)
    {
        Details = $"The keeper {keeperId.Value} is already assigned to maximum number of groups ({KeepingPolicy.MaxAssignmentsPerKeeper}).";
    }
}

#endregion