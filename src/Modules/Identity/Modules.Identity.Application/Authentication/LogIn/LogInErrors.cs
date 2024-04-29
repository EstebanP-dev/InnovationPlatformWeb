namespace Modules.Identity.Application.Authentication.LogIn;

public sealed class LogInErrors
{
    public static Error UnhandledRequest => Error.Unexpected(
        message: "An error occurred while processing the request.");
}
