namespace Modules.Projects.Application.Projects.GetTotalStatusCountByUser;

public sealed class StatusResponse
{
    [JsonPropertyName("status")]
    public string? Name { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("last_created")]
    public DateTime LastCreated { get; set; }
}
