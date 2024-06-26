﻿namespace Modules.Projects.Application.Projects.GetProjects;

public sealed class GetProjectsDeliverableResponse
{
    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
