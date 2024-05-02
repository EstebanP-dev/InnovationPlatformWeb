namespace Modules.Projects.Application.GetAuthors;

public sealed class GetAuthorsResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    [JsonPropertyName("full_name")]
    public string? Name { get; set; }
}
