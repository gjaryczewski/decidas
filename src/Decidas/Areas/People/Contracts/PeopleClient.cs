using Decidas.Areas.People.Features;

namespace Decidas.Areas.People.Contracts;

public class PeopleClient(GetKeeperListQuery _keeperList)
{
    public async Task<List<KeeperType>> GetKeeperList(CancellationToken cancel)
    {
        var request = new GetKeeperListRequest(1, 100);

        try
        {
            return (await _keeperList.ExecuteAsync(request, cancel)).Items;
        }
        catch
        {
            return [];
        }
    }
}
