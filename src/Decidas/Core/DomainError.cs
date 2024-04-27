using System.Net;

namespace Decidas.Core;

public class DomainError : Exception
{
    public string Details { get; internal set; } = string.Empty;

    public DateTime Timestamp { get; internal set; } = DateTime.UtcNow;

    public override String Message {
        get
        {
            return $"{Details} | Timestamp: {Timestamp:s}";
        }
    }
}
