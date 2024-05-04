namespace Modules.Projects.Application.Assessors.GetAssessors;

public sealed record GetAssessorsQuery()
    : IQuery<IEnumerable<GetAssessorsResponse>>;
