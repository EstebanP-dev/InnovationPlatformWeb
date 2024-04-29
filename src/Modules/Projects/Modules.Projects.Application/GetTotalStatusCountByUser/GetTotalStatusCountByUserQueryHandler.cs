using SharedKernel.Primitives;

namespace Modules.Projects.Application.GetTotalStatusCountByUser;

internal sealed class GetTotalStatusCountByUserQueryHandler(
        IProjectsClient client)
    : IQueryHandler<GetTotalStatusCountByUserQuery, GetTotalStatusCountByUserResponse>
{
    public async Task<Result<GetTotalStatusCountByUserResponse>> Handle(
        GetTotalStatusCountByUserQuery request,
        CancellationToken cancellationToken)
    {
        var result = await client
            .GetTotalStateCountByUserAsync()
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<GetTotalStatusCountByUserResponse>(errors);
        }

        var response = result.Value;

        if (response is null)
        {
            return Result.Failure<GetTotalStatusCountByUserResponse>(GetTotalStatusCountByUserErrors
                .UnexpectedError);
        }

        return Result.Success(response);
    }
}
