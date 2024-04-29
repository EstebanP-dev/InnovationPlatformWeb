using Modules.Projects.Application.Enumerations;
using Modules.Projects.Application.GetProjects;

namespace Modules.Projects.Presentation.Projects.All.List;

public sealed record ProjectListViewModel(
    string Id,
    ProjectStatusEnumeration? Status,
    string Title,
    string Description,
    ProjectTypeEnumeration? Type,
    MemberViewModel Assessor,
    IEnumerable<MemberViewModel> Authors,
    IEnumerable<DeliverableViewModel> Deliverables,
    DateTime? CreatedAt,
    DateTime? UpdatedAt)
{
    private static ProjectListViewModel FromResponse(GetProjectsResponse? response)
    {
        return new ProjectListViewModel(
            response?.Id ?? string.Empty,
            ProjectStatusEnumeration.FromName(response?.Status),
            response?.Title ?? string.Empty,
            response?.Description ?? string.Empty,
            ProjectTypeEnumeration.FromName(response?.Type),
            MemberViewModel.FromResponse(response?.Assessor),
            response?.Authors.Select(MemberViewModel.FromResponse) ?? [],
            response?.Deliverables.Select(DeliverableViewModel.FromResponse) ?? [],
            response?.CreatedAt,
            response?.UpdatedAt);
    }

    internal static IEnumerable<ProjectListViewModel> FromResponse(IEnumerable<GetProjectsResponse>? response)
    {
        return response?.Select(FromResponse) ?? [];
    }
};
