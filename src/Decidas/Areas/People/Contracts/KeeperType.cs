namespace Decidas.Areas.People.Contracts;

public record KeeperType(Guid Id, Guid MemberId, string Name, string Email, DateTime DesignateDate);
