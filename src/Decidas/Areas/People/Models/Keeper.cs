using Decidas.Core;
using Decidas.Shared;

namespace Decidas.Areas.People.Models;

public class Keeper
{
    public KeeperId Id { get; private set; } = default!;

    public MemberId MemberId { get; private set; } = default!;

    public Member Member { get; private set; } = default!;

    public DateOnly DesignateDate { get; private set; } = default!;

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
