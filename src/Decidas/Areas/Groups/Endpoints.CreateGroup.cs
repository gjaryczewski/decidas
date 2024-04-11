namespace Decidas.Areas.Groups.Endpoints;

public record CreateGroup(string Name, DateOnly StartDate, bool IsOpen);

public class CreateGroupHandler
{
    private readonly Database _db;

    public CreateGroupHandler
    public Group Handle(CreateGroup command, CancellationToken cancel)
    {
        _
    }
}