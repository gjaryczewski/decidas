namespace Decidas.Models;

public class Keeper
{
    public Guid Id { get; private set; }

    public Account Account { get; private set; }

    public Group Group { get; private set; }
}
