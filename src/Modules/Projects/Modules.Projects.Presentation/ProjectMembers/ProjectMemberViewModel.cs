namespace Modules.Projects.Presentation.ProjectMembers;

public sealed record ProjectMemberViewModel(
    string Id,
    string? Avatar,
    string Name)
{
    internal static IEnumerable<ProjectMemberViewModel> FromResponse()
    {
        return
        [
            new ProjectMemberViewModel("1", "images/faces/face1.jpg", "John Doe"),
            new ProjectMemberViewModel("2", "images/faces/face2.jpg", "Jane Doe"),
            new ProjectMemberViewModel("3", "images/faces/face3.jpg", "John Smith"),
            new ProjectMemberViewModel("4", "images/faces/face4.jpg", "Jane Smith"),
            new ProjectMemberViewModel("5", "images/faces/face5.jpg", "John Johnson"),
        ];
    }
};
