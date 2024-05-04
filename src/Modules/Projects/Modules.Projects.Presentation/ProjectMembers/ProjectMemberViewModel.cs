using Modules.Projects.Application.Assessors.GetAssessors;
using Modules.Projects.Application.Authors.GetAuthors;

namespace Modules.Projects.Presentation.ProjectMembers;

public sealed class ProjectMemberViewModel
{
    public string Id { get; set; } = string.Empty;

    public string? Avatar { get; set; }

    public string Name { get; set; } = string.Empty;

    private static ProjectMemberViewModel FromAssessorsResponse(GetAssessorsResponse? response)
    {
        return new ProjectMemberViewModel
        {
            Id = response?.Id ?? "", Avatar = response?.Avatar, Name = response?.Name ?? ""
        };
    }

    private static ProjectMemberViewModel FromAuthorsResponse(GetAuthorsResponse? response)
    {
        return new ProjectMemberViewModel
        {
            Id = response?.Id ?? "", Avatar = response?.Avatar, Name = response?.Name ?? ""
        };
    }

    internal static IEnumerable<ProjectMemberViewModel> FromAssessorsResponse(IEnumerable<GetAssessorsResponse>? responses)
    {
        return responses?.Select(FromAssessorsResponse) ?? [];
    }

    internal static IEnumerable<ProjectMemberViewModel> FromAuthorsResponse(IEnumerable<GetAuthorsResponse>? responses)
    {
        return responses?.Select(FromAuthorsResponse) ?? [];
    }
};
