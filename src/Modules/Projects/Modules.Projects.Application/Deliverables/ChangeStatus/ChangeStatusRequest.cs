namespace Modules.Projects.Application.Deliverables.ChangeStatus;

public sealed class ChangeStatusRequest
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }
}
