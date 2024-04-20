namespace Decidas.Models;

public class Group
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public string SocialLink { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? CeaseDate { get; set; }

    public bool IsActive { get; set; }

    public ICollection<Keeper> Keepers { get; set; }

    public ICollection<Member> Members { get; set; }
}
