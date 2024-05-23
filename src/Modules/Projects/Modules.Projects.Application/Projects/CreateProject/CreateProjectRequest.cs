namespace Modules.Projects.Application.Projects.CreateProject;

public sealed class CreateProjectRequest
{
    [JsonPropertyName("assessor")]
    public required string AssessorId { get; set; }

    [JsonPropertyName("type")]
    public required string TypeId { get; set; }

    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("folder")]
    public required string DeliverableFolder { get; set; }

    [JsonPropertyName("authors")]
    public required IEnumerable<string> AuthorIds { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }
}
