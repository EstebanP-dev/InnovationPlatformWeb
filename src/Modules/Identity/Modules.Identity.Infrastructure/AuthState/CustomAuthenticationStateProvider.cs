using System.Security.Claims;
using Common.Application.Storage;
using Modules.Identity.Application.Authentication;
using Modules.Identity.Domain.User;

namespace Modules.Identity.Infrastructure.AuthState;

internal sealed class CustomAuthenticationStateProvider(
        IAuthSessionStorage authSessionStorage)
    : AuthenticationStateProvider, IUserSessionClient
{
    private readonly ClaimsPrincipal _noneClaimsPrincipal = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userSession = await authSessionStorage
            .Get<UserSession>(AuthSessionKeys.UserSession)
            .ConfigureAwait(false);

        if (userSession is null)
        {
            return new AuthenticationState(_noneClaimsPrincipal);
        }

        var claimsPrincipal = CreateClaimsPrincipal(userSession);

        return new AuthenticationState(claimsPrincipal);
    }

    public async Task UpdateAuthenticationStateAsync(UserSession? userSession, CancellationToken cancellationToken = default)
    {
        if (userSession is null)
        {
            await authSessionStorage
                .Remove(AuthSessionKeys.Token, cancellationToken)
                .ConfigureAwait(false);

            await authSessionStorage
                .Remove(AuthSessionKeys.UserSession, cancellationToken)
                .ConfigureAwait(false);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_noneClaimsPrincipal)));

            return;
        }

        var claimsPrincipal = CreateClaimsPrincipal(userSession);

        await authSessionStorage
            .Save(AuthSessionKeys.UserSession, userSession, cancellationToken)
            .ConfigureAwait(false);

        ArgumentNullException.ThrowIfNull(userSession.Id);

        await authSessionStorage
            .Save(AuthSessionKeys.UserId, userSession.Id, cancellationToken)
            .ConfigureAwait(false);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private static ClaimsPrincipal CreateClaimsPrincipal(UserSession? userSession)
    {
        const string authenticationType = "JwtAuth";

        ArgumentNullException.ThrowIfNull(userSession);
        ArgumentNullException.ThrowIfNull(userSession.UserName);
        ArgumentNullException.ThrowIfNull(userSession.Email);

        var roleClaims = userSession.Roles
            .Select(x =>
            {
                ArgumentNullException.ThrowIfNull(x.Name);

                return new Claim(ClaimTypes.Role, x.Name);
            });

        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, userSession.UserName),
            new Claim(ClaimTypes.Email, userSession.Email),
            ..roleClaims
        ];

        return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType));
    }
}
