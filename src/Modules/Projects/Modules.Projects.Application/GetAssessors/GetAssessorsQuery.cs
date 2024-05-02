namespace Modules.Projects.Application.GetAssessors;

public sealed record GetAssessorsQuery()
    : IQuery<IEnumerable<GetAssessorsResponse>>;
