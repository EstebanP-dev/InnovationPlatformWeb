namespace Modules.Projects.Application.Projects.UpdateProject;

internal sealed class UpdateProjectCommandHandler(
        IProjectsClient client)
    : ICommandHandler<UpdateProjectCommand, Updated>
{
    public async Task<Result<Updated>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var updateRequest = new UpdateProjectRequest
        {
            AssessorId = request.AssessorId,
            TypeId = request.TypeId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status
        };

        var result = await client
            .UpdateProjectAsync(request.ProjectId, updateRequest)
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<Updated>(errors);
        }

        return Result.Success(Results.Updated);
    }
}
