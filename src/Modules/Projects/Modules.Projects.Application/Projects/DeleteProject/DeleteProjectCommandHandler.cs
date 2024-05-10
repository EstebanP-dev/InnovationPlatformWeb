namespace Modules.Projects.Application.Projects.DeleteProject;

internal sealed class DeleteProjectCommandHandler(
        IProjectsClient client)
    : ICommandHandler<DeleteProjectCommand, Deleted>
{
    public async Task<Result<Deleted>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var result = await client
            .DeleteProjectAsync(request.Id)
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<Deleted>(errors);
        }

        return Result.Success(Results.Deleted);
    }
}
