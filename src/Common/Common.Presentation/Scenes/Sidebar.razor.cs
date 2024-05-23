namespace Common.Presentation.Scenes;

public partial class Sidebar
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool Expanded { get; set; }

    [Parameter]
    public EventCallback<bool> ExpandedChanged { get; set; }
}
