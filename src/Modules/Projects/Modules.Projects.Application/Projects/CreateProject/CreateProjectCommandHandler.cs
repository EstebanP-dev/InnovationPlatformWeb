using Refit;

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

        var projectResponse = result.Value;

        if (string.IsNullOrWhiteSpace(projectResponse))
        {
            return Result.Failure<Created>(GeneralErrors.UnhandledRequest);
        }

        var deliverableTasks = request
            .Deliverables
            .Select(x =>
            {
                var deliverableRequest = CreateProjectCommand.ToDeliverableRequest(x);

                var file = x.File;

                using var fileStream = file.OpenReadStream();

                var streamPart = new StreamPart(fileStream, file.Name, file.ContentType);

                return client.CreateDeliverablesAsync(projectResponse, deliverableRequest, streamPart);
            });

        var deliverableResults = await Task
            .WhenAll(deliverableTasks)
            .ConfigureAwait(false);

        if (deliverableResults.Any(x => !x.IsSuccess()))
        {
            var errors = deliverableResults
                .SelectMany(x => x.MapErrors())
                .ToArray();

            return Result.Failure<Created>(errors);
        }


        return Result.Success(Results.Created);
    }
}
