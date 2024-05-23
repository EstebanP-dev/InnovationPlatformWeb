using Common.Presentation.Mvvm;


namespace Modules.Identity.Presentation.Authentication.LogIn;

public sealed partial class LogInViewModel
    : BaseViewModel
{
    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;
}
