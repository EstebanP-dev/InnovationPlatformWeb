namespace Modules.Projects.Application.Projects.DeleteProject;

public sealed record DeleteProjectCommand(string Id)
    : ICommand<Deleted>;
