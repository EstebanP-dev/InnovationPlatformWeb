using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Modules.Projects.Application.Projects.CreateProject;

internal sealed class CreateProjectCommandHandler(
        IProjectsClient client)
    : ICommandHandler<CreateProjectCommand, Created>
{
    public async Task<Result<Created>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var content = GetMultipartContent(request);

        var result = await client
            .CreateProjectAsync(content)
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<Created>(errors);
        }

        return Result.Success(Results.Created);
    }

    private static MultipartFormDataContent GetMultipartContent(CreateProjectCommand command)
    {
        MultipartFormDataContent content = [];

        var request = CreateProjectCommand.ToRequest(command);

        using var requestContent = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        content.Add(requestContent);

        foreach (var deliverable in command.Deliverables)
        {
            using var filesContent = new StreamContent(deliverable.File);

            filesContent.Headers.ContentType = new MediaTypeHeaderValue(deliverable.ContentType);

            var extension = Path.GetExtension(deliverable.Filename);

            content.Add(filesContent, "files", deliverable.Identifier + "." + extension);
        }

        return content;
    }
}
