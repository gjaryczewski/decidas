namespace Decidas.Models;

public class Topic
{
    public Guid Id { get; private set; }

    public byte Order { get; private set; }

    public string Title { get; private set; }

    public string ReferenceLink { get; private set; }
}
