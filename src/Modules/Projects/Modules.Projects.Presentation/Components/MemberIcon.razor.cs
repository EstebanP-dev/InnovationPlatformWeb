namespace Modules.Projects.Presentation.Components;

public sealed partial class MemberIcon
{
    [Parameter]
    public string? Avatar { get; set; }

    [Parameter]
    public string? Name { get; set; }

    private string GetInitials()
    {
        if (!string.IsNullOrWhiteSpace(Avatar))
        {
            return Avatar;
        }

        if (string.IsNullOrWhiteSpace(Name))
        {
            return "--.--";
        }

        var names = Name.Split(' ');

        return names.Length switch
        {
            1 => $"{names[0][0]}{names[0][1]}",
            _ => $"{names[0][0]}{names[1][0]}."
        };
    }
}
