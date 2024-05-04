using Modules.Projects.Application.Types.GetProjectTypes;

namespace Modules.Projects.Presentation.Types;

public sealed class ProjectTypeViewModel
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    private static ProjectTypeViewModel FromResponse(GetProjectTypesResponse? response)
    {
        return new ProjectTypeViewModel
        {
            Id = response?.Id,
            Name = response?.Name
        };
    }

    internal static IEnumerable<ProjectTypeViewModel> FromResponse(IEnumerable<GetProjectTypesResponse>? responses)
    {
        return responses?.Select(FromResponse) ?? [];
    }
}
