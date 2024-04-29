using Modules.Projects.Application.Enumerations;

namespace Modules.Projects.Presentation.Projects.All.List.Table;

public sealed partial class TypeBadge
{
    [Parameter]
    public ProjectTypeEnumeration? Type { get; set; }

    private string CssClass =>
        "text-lg text-blue-600 mdi mdi-" +
        Type?.Icon;
}
