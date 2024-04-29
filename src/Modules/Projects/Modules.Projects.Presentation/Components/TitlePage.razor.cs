using Microsoft.JSInterop;

namespace Modules.Projects.Presentation.Components;

public sealed partial class TitlePage
{
    private readonly HashSet<(string, bool)> _breadCrumbs = new();

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public string? Route { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    [Parameter]
    public bool ShowBackButton { get; set; }

    [Parameter]
    public bool ShowTileButton { get; set; }

    [Parameter]
    public string? TailButtonTitle { get; set; }

    [Parameter]
    public string? TailButtonIcon { get; set; }

    [Parameter]
    public EventCallback TailButtonOnClick { get; set; }

    private string CssClassString => $"text-2xl font-semibold capitalize {CssClass}" ;

    [Inject]
    private NavigationManager? NavigationManager { get; init; }

    [Inject]
    private IJSRuntime? JSRuntime { get; init; }


    protected override void OnInitialized()
    {
        ArgumentNullException.ThrowIfNull(NavigationManager);

        var uri = NavigationManager.Uri;
        var path = Route ?? uri
            .Replace(
                NavigationManager.BaseUri,
                "",
                StringComparison.InvariantCultureIgnoreCase);

        var segments = path
            .Split('/');

        foreach (var segment in segments)
        {
            if (string.IsNullOrWhiteSpace(segment))
            {
                continue;
            }

            var isLast = segment == segments.Last();

            _breadCrumbs.Add((segment, isLast));
        }
    }

    private async Task GoBack()
    {
        ArgumentNullException.ThrowIfNull(JSRuntime);

        await JSRuntime
            .InvokeVoidAsync("history.back")
            .ConfigureAwait(false);
    }

    private async Task TailButtonOnClickHandler()
    {
        await TailButtonOnClick
            .InvokeAsync()
            .ConfigureAwait(false);
    }
}
