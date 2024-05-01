using Decidas.Core;
using Decidas.Shared;

namespace Decidas.Areas.People.Models;

public class Keeper : DomainEntity
{
    public KeeperId Id { get; private set; } = default!;

    public MemberId MemberId { get; private set; } = default!;

    public Member Member { get; private set; } = default!;

    public DateOnly DesignateDate { get; private set; } = default!;

    public Keeper() {}

    public static Keeper Designate(Guid memberId, DateOnly designateDate)
    {
        var keeper = new Keeper
        {
            Id = new(Guid.NewGuid()),
            MemberId = new(memberId),
            DesignateDate = designateDate
        };

        keeper.AddDomainEvent(new KeeperDesignatedEvent(keeper.Id.Value));

        return keeper;
    }
}

public record KeeperId(Guid Value);

public record KeeperType(Guid Id, Guid MemberId, string Name, string Email, DateTime DesignateDate)
{
    public static KeeperType FromKeeper(Keeper keeper, Member member) => new(
        keeper.Id.Value,
        keeper.MemberId.Value,
        member.Name,
        member.Email.Value,
        keeper.DesignateDate.ToDateTime()
    );
}

public class KeeperDesignatedEvent(Guid id) : DomainEvent(id) {}
