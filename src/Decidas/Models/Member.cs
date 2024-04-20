namespace Decidas.Models;

public class Member
{
    public Guid Id { get; set; }

    public Account Account { get; set; }

    public Group Group { get; set; }
}
