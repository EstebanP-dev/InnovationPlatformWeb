namespace Modules.Projects.Application.GetAuthors;

internal sealed class GetAuthorsQueryHandler(
        IProjectsClient client)
    : IQueryHandler<GetAuthorsQuery, IEnumerable<GetAuthorsResponse>>
{
    public async Task<Result<IEnumerable<GetAuthorsResponse>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var result = await client
            .GetAuthorsAsync()
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<IEnumerable<GetAuthorsResponse>>(errors);
        }

        var data = result.Value;

        return data is null
            ? Result.Failure<IEnumerable<GetAuthorsResponse>>(ProjectErrors.NoAuthorsFound)
            : Result.Success(data);
    }
}
