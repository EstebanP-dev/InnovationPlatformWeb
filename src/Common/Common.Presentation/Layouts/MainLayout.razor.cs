namespace Common.Presentation.Layouts;

public sealed partial class MainLayout
{
    private List<string> _urlSegments = [];

    [Inject]
    private NavigationManager? NavigationManager { get; init; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        ArgumentNullException.ThrowIfNull(NavigationManager);

        var uri = new Uri(NavigationManager.Uri);
        _urlSegments = uri
            .AbsolutePath
            .Split('/', StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }
}
