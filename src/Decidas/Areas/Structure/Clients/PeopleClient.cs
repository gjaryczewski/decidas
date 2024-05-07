using Decidas.Areas.People;

namespace Decidas.Areas.Structure.Clients;

public class PeopleClient(GetKeeperListQuery _keeperList)
{
    public async Task<List<KeeperDetails>> GetKeeperListAsync(CancellationToken cancel)
    {
        var request = new KeeperListRequest(1, 100);

        try
        {
            return (await _keeperList.ExecuteAsync(request, cancel)).Items
                .Select(i => new KeeperDetails(
                    i.Id,
                    i.MemberId,
                    i.Name,
                    i.Email,
                    i.DesignateDate
                ))
                .ToList();
        }
        catch
        {
            return [];
        }
    }
}

#region Types

public record KeeperDetailsRequest(Guid Id);

public record KeeperDetails(Guid Id, Guid MemberId, string Name, string Email, DateTime DesignateDate);

#endregion
