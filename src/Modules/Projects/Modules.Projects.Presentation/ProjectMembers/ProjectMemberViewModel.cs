using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Modules.Projects.Application.Assessors.GetAssessors;
using Modules.Projects.Application.Authors.GetAuthors;
using Modules.Projects.Application.Projects.GetProject;

namespace Modules.Projects.Presentation.ProjectMembers;

public sealed class ProjectMemberViewModel : BaseViewModel
{
    public string Id { get; set; } = string.Empty;

    public string? Avatar { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool CanRemoveItem { get; set; } = true;


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

    internal static ProjectMemberViewModel FromGetProjectMemberResponse(GetProjectMemberResponse? response)
    {
        return new ProjectMemberViewModel
        {
            Id = response?.Id ?? "",
            Name = response?.FullName ?? ""
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
