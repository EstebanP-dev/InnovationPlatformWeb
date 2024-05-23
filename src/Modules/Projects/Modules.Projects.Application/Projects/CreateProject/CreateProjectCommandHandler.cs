namespace Modules.Projects.Application.Projects.CreateProject;

internal sealed class CreateProjectCommandHandler(
        IProjectsClient client)
    : ICommandHandler<CreateProjectCommand, Created>
{
    public async Task<Result<Created>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var result = await client
            .CreateProjectAsync(CreateProjectCommand.ToRequest(request))
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<Created>(errors);
        }

        var projectId = result.Value;

        if (string.IsNullOrWhiteSpace(projectId))
        {
            return Result.Failure<Created>(GeneralErrors.UnhandledRequest);
        }

        foreach (var deliverable in request.Deliverables)
        {
            var deliverableRequest = CreateProjectCommand.ToDeliverableRequest(deliverable);

            var deliverableResult = await client.CreateDeliverablesAsync(
                    projectId,
                    deliverableRequest)
                .ConfigureAwait(false);

            if (!deliverableResult.IsSuccess())
            {
                continue;
            }
        }

        return Result.Success(Results.Created);
    }
}
