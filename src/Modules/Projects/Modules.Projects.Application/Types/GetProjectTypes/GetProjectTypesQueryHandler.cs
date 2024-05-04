namespace Modules.Projects.Application.Types.GetProjectTypes;

internal sealed class GetProjectTypesQueryHandler(
        IProjectsClient client)
    : IQueryHandler<GetProjectTypesQuery, IEnumerable<GetProjectTypesResponse>>
{
    public async Task<Result<IEnumerable<GetProjectTypesResponse>>> Handle(GetProjectTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await client
            .GetProjectTypesAsync()
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<IEnumerable<GetProjectTypesResponse>>(errors);
        }

        var data = result.Value;

        return data is null
            ? Result.Failure<IEnumerable<GetProjectTypesResponse>>(ProjectErrors.NoAuthorsFound)
            : Result.Success(data);
    }
}
