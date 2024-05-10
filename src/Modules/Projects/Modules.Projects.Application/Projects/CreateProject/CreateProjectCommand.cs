namespace Modules.Projects.Application.Projects.CreateProject;

public sealed record CreateProjectCommand(
    string Assessor,
    string Type,
    string Title,
    string? Description,
    IEnumerable<string> Authors,
    IEnumerable<CreateProjectDeliverableDto> Deliverables,
    string Status)
    : ICommand<Created>
{
    internal static CreateProjectDeliverableRequest ToDeliverableRequest(CreateProjectDeliverableDto deliverable) =>
        new()
        {
            Id = deliverable.Identifier,
            TypeId = deliverable.Type,
            Name = deliverable.Name,
            Description = deliverable.Description
        };

    internal static CreateProjectRequest ToRequest(CreateProjectCommand command) =>
        new()
        {
            AssessorId = command.Assessor,
            TypeId = command.Type,
            Title = command.Title,
            Description = command.Description,
            AuthorIds = command.Authors,
            Status = command.Status
        };
}
