namespace Modules.Projects.Application.Projects.GetProjects;

public sealed record GetProjectsQuery
    : IQuery<IEnumerable<GetProjectsResponse>>;
