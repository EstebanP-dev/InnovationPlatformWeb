namespace Modules.Projects.Application.GetTotalStatusCountByUser;

public sealed class GetTotalStatusCountByUserResponse
{
    [JsonPropertyName("total_projects")]
    public int Total { get; set; }

    [JsonPropertyName("status")]
    public IEnumerable<StatusResponse> Status { get; set; } = [];
}
