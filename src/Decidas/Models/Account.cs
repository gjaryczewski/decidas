namespace Decidas.Models;

public class Account
{
    public Guid Id { get; set; }

    public string Login { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public DateTime RegisterTime { get; set; }
}
