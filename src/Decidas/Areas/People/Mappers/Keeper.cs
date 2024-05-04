using Decidas.Areas.People.Contracts;
using Decidas.Areas.People.Models;
using Decidas.Shared;

namespace Decidas.Areas.People.Mappers;

public static class KeeperMappers
{
    public static KeeperType ToKeeperType(this Keeper keeper) => new(
        keeper.Id.Value,
        keeper.MemberId.Value,
        keeper.Member.Name,
        keeper.Member.Email.Value,
        keeper.DesignateDate.ToDateTime()
    );
}