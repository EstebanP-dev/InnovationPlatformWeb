namespace Modules.Projects.Application.Projects.UpdateProject;

public record UpdateProjectCommand(
    string ProjectId,
    string? AssessorId = null,
    string? TypeId = null,
    string? Title = null,
    string? Description = null,
    string? Status = null) : ICommand<Updated>;
