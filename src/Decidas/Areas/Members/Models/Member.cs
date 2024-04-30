using Decidas.Core;
using Decidas.Shared;

namespace Decidas.Areas.Members.Models;

public class Member : DomainEntity
{
    public MemberId Id { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public Login Login { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    public PasswordHash PasswordHash { get; private set; } = default!;

    public DateOnly RegisterDate { get; private set; } = default!;

    public Member() {}

    public static Member Create(string name, string login, string email, string password)
    {
        var group = new Member
        {
            Id = new(Guid.NewGuid()),
            Name = name,
            Login = new(login),
            Email = new(email),
            PasswordHash = new(password),
            RegisterDate = DateOnly.FromDateTime(DateTime.Today)
        };

        group.AddDomainEvent(new MemberCreatedEvent(group.Id.Value));

        return group;
    }
}

public record Login
{
    public string Value { get; }

    public Login(string login)
    {
        Value = login;
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

public record MemberType(Guid Id, string Name, string Login, string Email, DateTime RegisterDate)
{
    public static MemberType FromMember(Member member) => new(
        member.Id.Value,
        member.Name,
        member.Login.Value,
        member.Email.Value,
        member.RegisterDate.ToDateTime()
    );
}

public class MemberCreatedEvent(Guid id) : DomainEvent(id) {}
