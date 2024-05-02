namespace Modules.Projects.Application.GetProjectTypes;

public sealed class GetProjectTypesResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
