namespace Modules.Projects.Application.DeliverableTypes.GetDeliverableTypes;

internal sealed class GetDeliverableTypesQueryHandler(
        IProjectsClient client)
    : IQueryHandler<GetDeliverableTypesQuery, IEnumerable<GetDeliverableTypesResponse>>
{
    public async Task<Result<IEnumerable<GetDeliverableTypesResponse>>> Handle(GetDeliverableTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await client
            .GetDeliverableTypesAsync()
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<IEnumerable<GetDeliverableTypesResponse>>(errors);
        }

        var response = result.Value;

        return response is null
            ? Result.Failure<IEnumerable<GetDeliverableTypesResponse>>(GeneralErrors.UnhandledRequest)
            : Result.Success(response);
    }
}
