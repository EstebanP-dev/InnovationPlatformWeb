namespace Modules.Projects.Application.Projects.GetProject;

public sealed record GetProjectQuery(string Id)
    : IQuery<GetProjectResponse>;
