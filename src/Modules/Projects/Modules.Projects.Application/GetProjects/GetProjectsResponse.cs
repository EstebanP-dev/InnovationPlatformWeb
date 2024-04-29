namespace Modules.Projects.Application.GetProjects;


public sealed class GetProjectsResponse
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

    [JsonPropertyName("assessor")]

    public MemberResponse? Assessor { get; set; }

    [JsonPropertyName("authors")]
    public IEnumerable<MemberResponse> Authors { get; set; } = [];

    [JsonPropertyName("deliverables")]
    public IEnumerable<DeliverableResponse> Deliverables { get; set; } = [];

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
