namespace Modules.Projects.Application.DeliverableTypes.GetDeliverableTypes;

public sealed class GetDeliverableTypesResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("extension")]
    public string? Extension { get; set; }
}
