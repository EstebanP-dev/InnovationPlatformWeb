using Modules.Projects.Application.Enumerations;

namespace Modules.Projects.Presentation.Projects.All.List.Table;

public sealed partial class StatusBadge
{
    [Parameter]
    public ProjectStatusEnumeration? Status { get; set; }

    private string CssClass =>
        "w-4 h-4 rounded-full " +
        Status?.BackgroundColor ?? "";
}
