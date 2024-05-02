namespace Modules.Projects.Application.GetAssessors;

internal sealed class GetAssessorsQueryHandler(
        IProjectsClient client)
    : IQueryHandler<GetAssessorsQuery, IEnumerable<GetAssessorsResponse>>
{
    public async Task<Result<IEnumerable<GetAssessorsResponse>>> Handle(GetAssessorsQuery request, CancellationToken cancellationToken)
    {
        var result = await client
            .GetAssessorsAsync()
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<IEnumerable<GetAssessorsResponse>>(errors);
        }

        var data = result.Value;

        return data is null
            ? Result.Failure<IEnumerable<GetAssessorsResponse>>(ProjectErrors.NoAssessorsFound)
            : Result.Success(data);
    }
}
