namespace Modules.Projects.Presentation.Scenes;

public partial class SidebarItem
{

    [Parameter]
    public string? Icon { get; set; }

    private string IconClass => $"mdi mdi-{Icon}";

    [Parameter]
    public string? Text { get; set; }

    [Parameter]
    public string? Route { get; set; }
}
