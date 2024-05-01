namespace Modules.Projects.Presentation.Projects.All.List;

public sealed class ProjectListCollection() : FilteredObservableCollection<ProjectListViewModel>(
    searchCriteria: (project, text) => project.Title.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                                       project.Description.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                                       (project.Status?.Name ?? "").Contains(text,
                                           StringComparison.OrdinalIgnoreCase) ||
                                       (project.Type?.Name ?? "").Contains(text, StringComparison.OrdinalIgnoreCase) ||
                                       project.Assessor.FullName.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                                       project.Authors.Any(x =>
                                           x.FullName.Contains(text, StringComparison.OrdinalIgnoreCase)) ||
                                       (project.CreatedAt is not null &&
                                        project.CreatedAt.ToString()!.Contains(text,
                                            StringComparison.OrdinalIgnoreCase)));
