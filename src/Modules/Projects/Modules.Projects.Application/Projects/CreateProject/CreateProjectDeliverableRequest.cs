using Refit;

namespace Modules.Projects.Application.Projects.CreateProject;

public sealed class CreateProjectDeliverableRequest
{
    [JsonPropertyName("url")]
    public required string Link { get; set; }

    [JsonPropertyName("type")]
    public required string TypeId { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
