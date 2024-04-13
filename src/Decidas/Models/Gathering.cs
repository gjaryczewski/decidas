namespace Decidas.Models;

public class Gathering
{
    public Guid Id { get; private set; }

    public DateTime Time { get; private set; }

    public string Place { get; private set; }

    public string MapLink { get; private set; }

    public Group Group { get; private set; }

    public Topic Topic { get; private set; }
}
