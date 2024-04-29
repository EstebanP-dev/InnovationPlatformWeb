using Common.Presentation;

namespace Modules.Identity.Presentation.Authentication.Unauthorized;

public partial class UnauthorizedComponent
{
    [Inject]
    private AuthenticationStateProvider? AuthenticationStateProvider { get; init; }

    [Inject]
    private NavigationManager? NavigationManager { get; init; }

    protected override async Task OnInitializedAsync()
    {
        ArgumentNullException.ThrowIfNull(AuthenticationStateProvider);
        ArgumentNullException.ThrowIfNull(NavigationManager);

        await base.OnInitializedAsync().ConfigureAwait(false);

        var authState = await AuthenticationStateProvider
            .GetAuthenticationStateAsync()
            .ConfigureAwait(false);

        if (authState.User.Identity?.IsAuthenticated == false)
        {
            NavigationManager.NavigateTo(RouteKeys.LogIn, replace: true);
        }
    }
}
