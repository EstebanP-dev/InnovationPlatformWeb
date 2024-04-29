namespace Common.Presentation.Components;

public partial class TableCell
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool ShowOnMobile { get; set; } = true;

    [Parameter]
    public bool Remarkable { get; set; }

    [Parameter]
    public bool IsLastest { get; set; }

    public string CssClass =>
        "px-6 py-4 " +
        (ShowOnMobile ? "" : "hidden md:block") +
        (IsLastest ? "text-right" : "");
}
