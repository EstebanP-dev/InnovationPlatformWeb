namespace Common.Presentation.Components;

public partial class TableHeader
{
    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public bool ShowOnMobile { get; set; } = true;

    private string CssClass => "px-6 py-3 " + (ShowOnMobile ? "" : "hidden md:block");
}
