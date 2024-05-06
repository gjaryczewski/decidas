using Decidas.Core;
using Decidas.Shared;

namespace Decidas.Areas.People.Models;

public class Member : DomainEventPublisher
{
    public MemberId Id { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    public PasswordHash PasswordHash { get; private set; } = default!;

    public DateOnly RegisterDate { get; private set; } = default!;

    public Keeper? Keeper { get; private set; }

    public Member() {}

    public static Member Register(string name, string email, string password)
    {
        var member = new Member
        {
            Id = new(Guid.NewGuid()),
            Name = name,
            Email = new(email),
            PasswordHash = new(password),
            RegisterDate = DateOnly.FromDateTime(DateTime.Today)
        };

        member.PublishDomainEvent(new MemberRegisteredEvent(member.Id.Value));

        return member;
    }

    public Keeper Designate(DateOnly designateDate)
    {
        var keeper = Keeper.Create(Id, designateDate);

        keeper.PublishDomainEvent(new KeeperDesignatedEvent(keeper.Id.Value));

        return keeper;
    }
}

public record Email
{
    public string Value { get; }

    public Email(string email)
    {
        Value = email;
    }
}

public record PasswordHash
{
    public string Value { get; }

    public PasswordHash(string password)
    {
        Value = password;
    }
}

public record MemberId(Guid Value);

public record MemberType(Guid Id, string Name, string Email, DateTime RegisterDate)
{
    public static MemberType FromMember(Member member) => new(
        member.Id.Value,
        member.Name,
        member.Email.Value,
        member.RegisterDate.ToDateTime()
    );
}

public class MemberRegisteredEvent(Guid id) : DomainEvent(id) {}

public class KeeperDesignatedEvent(Guid id) : DomainEvent(id) {}
