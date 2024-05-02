namespace Modules.Projects.Application.GetProjects;

internal sealed class GetProjectsQueryHandler(
        IProjectsClient client)
    : IQueryHandler<GetProjectsQuery, IEnumerable<GetProjectsResponse>>
{
    public async Task<Result<IEnumerable<GetProjectsResponse>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var result = await client
            .GetProjectsAsync()
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<IEnumerable<GetProjectsResponse>>(errors);
        }

        var data = result.Value;

        return data is null
            ? Result.Failure<IEnumerable<GetProjectsResponse>>(Error.Unexpected())
            : Result.Success(data);
    }
}
