namespace Decidas.Core;

public class DomainError : Exception
{
    public string Details { get; protected set; } = string.Empty;

    public DateTime Timestamp { get; protected set; } = DateTime.UtcNow;

    public override String Message {
        get
        {
            return $"{Details} | Timestamp: {Timestamp:s}";
        }
    }
}
