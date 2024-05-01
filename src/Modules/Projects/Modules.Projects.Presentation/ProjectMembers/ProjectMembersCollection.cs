namespace Modules.Projects.Presentation.ProjectMembers;

public sealed class ProjectMembersCollection() : FilteredObservableCollection<ProjectMemberViewModel>(
        searchCriteria: (member, text) => member.Name.Contains(text, StringComparison.OrdinalIgnoreCase));
