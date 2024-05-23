using Common.Domain.Errors;
using Modules.Identity.Application.Authentication.LogIn;
using Modules.Identity.Domain.Authentication;
using SharedKernel.Primitives;

namespace Modules.Identity.Presentation.Authentication.LogIn;

public sealed partial class Index
{
    private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();
    private readonly LogInViewModel _viewModel = new();

    [Inject]
    private AuthenticationStateProvider? AuthenticationStateProvider { get; init; }

    [Inject]
    private NavigationManager? NavigationManager { get; init; }

    [Inject]
    private ISender? Sender { get; init; }

    [Inject]
    private ISnackbar? Snackbar { get; init; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        ValidateAuthenticationState();
    }

    private void ValidateAuthenticationState()
    {
        ArgumentNullException.ThrowIfNull(AuthenticationStateProvider);
        ArgumentNullException.ThrowIfNull(NavigationManager);

        _dispatcher.InvokeAsync(async () =>
        {
            var authenticationState = await AuthenticationStateProvider
                .GetAuthenticationStateAsync()
                .ConfigureAwait(false);

            if (authenticationState.User.Identity?.IsAuthenticated ?? false)
            {
                NavigationManager.NavigateTo("/", replace: true);
            }
        });
    }

    private void OnValidSubmit(EditContext context)
    {
        ArgumentNullException.ThrowIfNull(NavigationManager);
        ArgumentNullException.ThrowIfNull(Snackbar);

        _dispatcher.InvokeAsync(async () =>
        {
            var result = await OnValidSubmitAsync()
                .ConfigureAwait(false);

            if (result.IsFailure)
            {
                Snackbar.Add(result.FirstError.Message, Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo("/", replace: true);
            }
        });
    }

    private async Task<Result<Success>> OnValidSubmitAsync()
    {
        ArgumentNullException.ThrowIfNull(AuthenticationStateProvider);
        ArgumentNullException.ThrowIfNull(Sender);

        if (_viewModel.IsBusy)
        {
            return Result.Failure<Success>(GeneralErrors.BusyUi);
        }

        _viewModel.IsBusy = true;

        var request = new LogInCommand(
            _viewModel.UserName,
            _viewModel.Password);

        var result = await Sender
            .Send(request)
            .ConfigureAwait(true);

        if (result.IsFailure)
        {
            return Result.Failure<Success>(result.FirstError);
        }

        var authenticationState = await AuthenticationStateProvider
            .GetAuthenticationStateAsync()
            .ConfigureAwait(false);

        _viewModel.IsBusy = false;

        if (authenticationState.User.Identity?.IsAuthenticated ?? false)
        {
            return Result.Success(Results.Success);
        }

        return Result.Failure<Success>(AuthenticationErrors.UnavailableToAuthenticate);
    }
}
