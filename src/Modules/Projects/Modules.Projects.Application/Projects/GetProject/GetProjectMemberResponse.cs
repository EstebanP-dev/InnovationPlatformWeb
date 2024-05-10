namespace Modules.Projects.Application.Projects.GetProject;

public sealed class GetProjectMemberResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }
}
