namespace Modules.Projects.Application.Projects.CreateProject;

public sealed record CreateProjectDeliverableDto(
    Guid Identifier,
    string Filename,
    string ContentType,
    string Type,
    string Name,
    FileStream File,
    string Description);
