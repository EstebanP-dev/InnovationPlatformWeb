using Modules.Projects.Application.Projects.UpdateProject;

namespace Modules.Projects.Application.Projects.ChangeProjectStatus;

internal sealed class ChangeProjectStatusCommandHandler(
        IProjectsClient client)
    : ICommandHandler<ChangeProjectStatusCommand, Success>
{
    public async Task<Result<Success>> Handle(ChangeProjectStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await client
            .UpdateProjectAsync(request.ProjectId, new UpdateProjectRequest
            {
                Status = request.Status
            })
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<Success>(errors);
        }

        return Result.Success(Results.Success);
    }
}
