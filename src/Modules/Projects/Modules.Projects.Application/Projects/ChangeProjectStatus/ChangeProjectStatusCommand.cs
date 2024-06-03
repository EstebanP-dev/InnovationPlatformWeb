namespace Modules.Projects.Application.Projects.ChangeProjectStatus;

public sealed record ChangeProjectStatusCommand(
    string ProjectId,
    string Status) : ICommand<Success>;
