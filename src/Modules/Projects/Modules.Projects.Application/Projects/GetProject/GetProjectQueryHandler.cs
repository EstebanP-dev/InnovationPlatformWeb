namespace Modules.Projects.Application.Projects.GetProject;

internal sealed class GetProjectQueryHandler(
        IProjectsClient client)
    : IQueryHandler<GetProjectQuery, GetProjectResponse>
{
    public async Task<Result<GetProjectResponse>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var result = await client
            .GetProjectAsync(request.Id)
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<GetProjectResponse>(errors);
        }

        var data = result.Value;

        return data is null
            ? Result.Failure<GetProjectResponse>(Error.Unexpected())
            : Result.Success(data);
    }
}
