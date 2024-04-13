namespace Decidas.Models;

public class Group
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string City { get; private set; }

    public string SocialLink { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly? CeaseDate { get; private set; }

    public bool IsActive { get; } => CeaseDate is null;

    public ICollection<Keeper> Keepers { get; private set; }

    public ICollection<Member> Members { get; private set; }
}
