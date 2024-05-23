namespace Modules.Projects.Application.Deliverables.ChangeStatus;

internal sealed class ChangeStatusCommandHandler(
        IProjectsClient client)
    : ICommandHandler<ChangeStatusCommand, Updated>
{
    public async Task<Result<Updated>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await client.ChangeDeliverableStatusAsync(
            request.ProjectId,
            request.DeliverableId,
            new ChangeStatusRequest { Status = request.Status })
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<Updated>(errors);
        }

        return Result.Success(Results.Updated);
    }
}
