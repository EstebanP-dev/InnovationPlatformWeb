namespace Modules.Projects.Application.GetProjectTypes;

public sealed record GetProjectTypesQuery()
    : IQuery<IEnumerable<GetProjectTypesResponse>>;
