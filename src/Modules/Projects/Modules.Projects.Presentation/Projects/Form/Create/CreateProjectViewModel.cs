using Modules.Projects.Presentation.Deliverables;
using Modules.Projects.Presentation.ProjectMembers;

namespace Modules.Projects.Presentation.Projects.Form.Create;

public sealed class CreateProjectViewModel
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

#pragma warning disable CA2227
    public ProjectMembersCollection Assessors { get; set; } = [];

    public ProjectMembersCollection Authors { get; set; } = [];

    public ProjectDeliverablesCollection Deliverables { get; set; } = [];
#pragma warning restore CA2227
}
