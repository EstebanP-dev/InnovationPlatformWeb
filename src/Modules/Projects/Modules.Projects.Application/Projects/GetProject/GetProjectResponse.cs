namespace Modules.Projects.Application.Projects.GetProject;


public sealed class GetProjectResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("deliverable_folder")]
    public string? DeliverableFolder { get; set; }

    [JsonPropertyName("assessor")]

    public GetProjectMemberResponse? Assessor { get; set; }

    [JsonPropertyName("authors")]
    public IEnumerable<GetProjectMemberResponse> Authors { get; set; } = [];

    [JsonPropertyName("deliverables")]
    public IEnumerable<GetProjectDeliverableResponse> Deliverables { get; set; } = [];

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
