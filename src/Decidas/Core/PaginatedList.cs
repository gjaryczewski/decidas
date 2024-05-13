namespace Decidas.Core;

public class PaginatedList<T>(int page, int perPage)
{
    public List<T> Items { get; } = [];

    public int Count => Items.Count;

    public int Page { get; init; } = page switch
    {
        <= 0 => 1,
        _ => page
    };

    public int PerPage { get; init; } = perPage switch
    {
        <= 0 => 30,
        > 100 => 100,
        _ => perPage,
    };
}
