namespace Modules.Identity.Application.Authentication;

public sealed class AuthenticationErrors
{
    public static Error UnhandledRequest => Error.Unexpected(
        message: "An error occurred while processing the request.");
}
