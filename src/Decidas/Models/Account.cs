namespace Decidas.Models;

public class Account
{
    public Guid Id { get; private set; }

    public string Login { get; private set; }

    public string Email { get; private set; }

    public string Name { get; private set; }

    public string Password { get; private set; }

    public DateTime RegisterTime { get; private set; }
}
