namespace Modules.Projects.Application.GetAuthors;

public sealed record GetAuthorsQuery()
    : IQuery<IEnumerable<GetAuthorsResponse>>;
