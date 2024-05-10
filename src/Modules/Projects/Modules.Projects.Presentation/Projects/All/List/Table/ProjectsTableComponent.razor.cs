namespace Modules.Projects.Presentation.Projects.All.List.Table;

public sealed partial class ProjectsTableComponent
{
    [Parameter]
#pragma warning disable CA2227
    public ProjectListCollection Data { get; set; } = [];
#pragma warning restore CA2227

    [Inject]
    private NavigationManager? NavigationManager { get; init; }
}
