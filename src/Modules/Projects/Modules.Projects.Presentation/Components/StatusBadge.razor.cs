namespace Modules.Projects.Presentation.Components;

public sealed partial class StatusBadge
{
    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public string? BackgroundColor { get; set; }

    private string CssClass =>
        "w-4 h-4 rounded-full " +
        BackgroundColor ?? "";
}
