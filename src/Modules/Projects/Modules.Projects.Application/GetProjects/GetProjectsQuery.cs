namespace Modules.Projects.Application.GetProjects;

public sealed record GetProjectsQuery
    : IQuery<IEnumerable<GetProjectsResponse>>;
