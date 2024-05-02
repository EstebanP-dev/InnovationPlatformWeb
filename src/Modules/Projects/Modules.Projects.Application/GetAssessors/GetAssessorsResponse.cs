namespace Modules.Projects.Application.GetAssessors;

public sealed class GetAssessorsResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    [JsonPropertyName("full_name")]
    public string? Name { get; set; }
}
