namespace Modules.Projects.Application.Types.GetProjectTypes;

public sealed record GetProjectTypesQuery()
    : IQuery<IEnumerable<GetProjectTypesResponse>>;
