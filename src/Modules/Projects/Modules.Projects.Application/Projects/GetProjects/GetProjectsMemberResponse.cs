namespace Modules.Projects.Application.Projects.GetProjects;

public sealed class MemberResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }
}
