using Modules.Identity.Application.Authentication.LogIn;
using Modules.Identity.Domain.User;

namespace Modules.Identity.Application.Authentication;

internal sealed class SetAuthenticationUserSessionHandler(
        IAuthenticationClient client,
        IUserSessionClient userSessionClient)
    : INotificationHandler<UserLoggedEvent>
{
    public async Task Handle(UserLoggedEvent notification, CancellationToken cancellationToken)
    {
        var result = await client
            .GetCurrentUser()
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            await userSessionClient
                .UpdateAuthenticationStateAsync(null, cancellationToken)
                .ConfigureAwait(false);

            return;
        }

        var user = result.Value;

        if (user is null)
        {
            await userSessionClient
                .UpdateAuthenticationStateAsync(null, cancellationToken)
                .ConfigureAwait(false);

            return;
        }

        var userSession = user.ToUserSession();

        await userSessionClient
            .UpdateAuthenticationStateAsync(userSession, cancellationToken)
            .ConfigureAwait(false);
    }



}
