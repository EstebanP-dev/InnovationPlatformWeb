namespace Common.Presentation.Scenes;

public partial class SidebarItem
{

    [Parameter]
    public string? Icon { get; set; }

    private string IconClass => $"mdi mdi-{Icon}";

    [Parameter]
    public string? Text { get; set; }

    [Parameter]
    public string? Route { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; init; }

    private void NavigateTo()
    {
        ArgumentNullException.ThrowIfNull(NavigationManager);

        if (string.IsNullOrWhiteSpace(Route))
        {
            return;
        }

        NavigationManager.NavigateTo(Route, true);
    }
}
