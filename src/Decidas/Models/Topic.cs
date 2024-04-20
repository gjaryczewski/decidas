namespace Decidas.Models;

public class Topic
{
    public Guid Id { get; set; }

    public byte Order { get; set; }

    public string Title { get; set; }

    public string ReferenceLink { get; set; }
}
