namespace Decidas.Core;

public class DomainError : Exception
{
    public string Location { get; internal set; } = string.Empty;

    public string Code { get; internal set; } = string.Empty;

    public string Details { get; internal set; } = string.Empty;

    public DateTime Timestamp { get; internal set; } = DateTime.UtcNow;
}
