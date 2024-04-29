using Modules.Identity.Application.Authentication.LogIn;

namespace Modules.Identity.Presentation.Authentication.LogIn;

public sealed partial class Index
{
    private readonly LogInViewModel _credentials = new();

    [Inject]
    private NavigationManager? NavigationManager { get; init; }

    [Inject]
    private ISender? Sender { get; init; }

    [Inject]
    private IToastService? ToastService { get; init; }

    private async Task OnValidSubmitAsync(EditContext context)
    {
        ArgumentNullException.ThrowIfNull(NavigationManager);
        ArgumentNullException.ThrowIfNull(Sender);
        ArgumentNullException.ThrowIfNull(ToastService);

        var request = new LogInCommand(
            _credentials.UserName,
            _credentials.Password);

        var result = await Sender
            .Send(request)
            .ConfigureAwait(true);

        if (result.IsFailure)
        {
            ToastService.ShowError(result.FirstError.Message);
        }
        else
        {
            NavigationManager.NavigateTo("/", replace: true);
        }

    }
}
