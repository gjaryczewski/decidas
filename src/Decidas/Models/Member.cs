namespace Decidas.Models;

public class Member
{
    public Guid Id { get; private set; }

    public Acccount Account { get; private set; }

    public Group Group { get; private set; }s
}
