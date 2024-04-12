namespace Decodas.Domain.Groups;

public record GroupsFeatures(
    bool CloseGroup = true,
    bool CreateGroup = true,
    bool GetGroup = true,
    bool ListGroup = true,
    bool RenameGroup = true,
    bool SetGroupAttributes = true
);

