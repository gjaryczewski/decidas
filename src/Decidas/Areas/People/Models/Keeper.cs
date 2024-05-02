using Decidas.Areas.People.Contracts;
using Decidas.Areas.Structure.Contracts;
using Decidas.Core;
using Decidas.Shared;

namespace Decidas.Areas.People.Models;

public class Keeper : DomainEntity
{
    public KeeperId Id { get; private set; } = default!;

    public MemberId MemberId { get; private set; } = default!;

    public Member Member { get; private set; } = default!;

    public DateOnly DesignateDate { get; private set; } = default!;

    public List<Assignment> Assignments { get; private set; } = default!;

    public Keeper() {}

    public static Keeper Create(MemberId memberId, DateOnly designateDate)
    {
        var keeper = new Keeper
        {
            Id = new(Guid.NewGuid()),
            MemberId = memberId,
            DesignateDate = designateDate
        };

        return keeper;
    }
}

public record KeeperId(Guid Value);

public record KeeperType(Guid Id, Guid MemberId, string Name, string Email, DateTime DesignateDate)
{
    public static KeeperType FromKeeper(Keeper keeper) => new(
        keeper.Id.Value,
        keeper.MemberId.Value,
        keeper.Member.Name,
        keeper.Member.Email.Value,
        keeper.DesignateDate.ToDateTime()
    );
}
