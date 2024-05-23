using Common.Application.Files;

namespace Modules.Projects.Application.Projects.CreateProject;

public sealed record CreateProjectDeliverableDto(
    string Type,
    string Status,
    string Name,
    string File,
    string Description);
