namespace Modules.Projects.Application.Projects.UpdateProject;

public class UpdateProjectRequest
{
    [JsonPropertyName("assessor")]
    public string? AssessorId { get; set; }

    [JsonPropertyName("type")]
    public string? TypeId { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
