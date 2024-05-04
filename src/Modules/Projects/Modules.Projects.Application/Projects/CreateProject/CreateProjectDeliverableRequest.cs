using Refit;

namespace Modules.Projects.Application.Projects.CreateProject;

public sealed class CreateProjectDeliverableRequest
{
    [JsonPropertyName("identifier")]
    public required Guid Id { get; set; }

    [JsonPropertyName("type")]
    public required string TypeId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
