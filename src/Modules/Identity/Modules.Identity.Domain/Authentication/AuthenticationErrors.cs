namespace Modules.Identity.Domain.Authentication;

public sealed class AuthenticationErrors
{
    public static readonly Error UnavailableToAuthenticate = Error.Unexpected(
        message: "Ha fallado la autenticación.");
}
