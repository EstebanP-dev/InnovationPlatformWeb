namespace Modules.Projects.Presentation.Projects.Form;

public sealed partial class MembersInformation
{
    [Parameter]
    public string? AddButtonTitle { get; set; }

    [Parameter]
    public int MaximumMembers { get; set; } = 3;

    [Parameter]
    public string? Title { get; set; }
}
