namespace Modules.Projects.Presentation.Deliverables;

public sealed class ProjectDeliverableViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

#pragma warning disable CA1056
    public string Url { get; set; } = string.Empty;
#pragma warning restore CA1056
}
