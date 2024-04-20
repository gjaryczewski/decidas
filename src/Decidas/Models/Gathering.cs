namespace Decidas.Models;

public class Gathering
{
    public Guid Id { get; set; }

    public DateTime Time { get; set; }

    public string Place { get; set; }

    public string MapLink { get; set; }

    public Group Group { get; set; }

    public Topic Topic { get; set; }
}
