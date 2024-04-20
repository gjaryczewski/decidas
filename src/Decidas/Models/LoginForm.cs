using System.Security.Cryptography;
using System.Text;

namespace Decidas.Models;

public class LoginForm
{
    public string Login { get; set; }
    public string Password { get; set; }

    public string PasswordHash()
    {
        return Password;
    }
}
