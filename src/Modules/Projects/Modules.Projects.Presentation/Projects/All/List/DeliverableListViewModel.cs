using Modules.Projects.Application.Projects.GetProjects;

namespace Modules.Projects.Presentation.Projects.All.List;

public sealed record DeliverableViewModel(
    Uri Url,
    string Name,
    string Type,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    internal static DeliverableViewModel FromResponse(GetProjectsDeliverableResponse? deliverable)
    {
        return new DeliverableViewModel(
            deliverable?.Url ?? new Uri(string.Empty),
            deliverable?.Name ?? string.Empty,
            deliverable?.Type ?? string.Empty,
            deliverable?.CreatedAt ?? DateTime.MinValue,
            deliverable?.UpdatedAt ?? DateTime.MinValue);
    }
};
