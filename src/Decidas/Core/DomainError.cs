namespace Decidas.Core;

internal class DomainError : Exception
{
    public string Location { get; internal set; } = string.Empty;

    public string Code { get; internal set; } = string.Empty;

    public string Details { get; internal set; } = string.Empty;

    public DateTime Timestamp { get; internal set; } = DateTime.UtcNow;
}
