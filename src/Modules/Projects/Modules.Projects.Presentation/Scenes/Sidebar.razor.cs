namespace Modules.Projects.Presentation.Scenes;

public partial class Sidebar
{
    [Parameter]
    public RenderFragment? Children { get; set; }

    private bool Expanded { get; set; } = true;
}
