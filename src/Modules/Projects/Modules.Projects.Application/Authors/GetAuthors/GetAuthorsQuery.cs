namespace Modules.Projects.Application.Authors.GetAuthors;

public sealed record GetAuthorsQuery()
    : IQuery<IEnumerable<GetAuthorsResponse>>;
