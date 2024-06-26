﻿namespace Modules.Projects.Application.Projects.GetProjects;

public sealed class GetProjectsMemberResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }
}
