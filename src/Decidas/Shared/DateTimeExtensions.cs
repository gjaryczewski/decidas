namespace Decidas.Shared;

public static class DateOnlyExtensions
{
    public static DateTime ToDateTime(this DateOnly date)
    {
        return date.ToDateTime(TimeOnly.MinValue);
    }
}