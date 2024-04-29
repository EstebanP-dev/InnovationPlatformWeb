using Common.Application.Storage;

namespace Common.Infrastructure.DelegatingHandlers;

internal sealed class HeaderDelegatingHandler(IAuthSessionStorage authSessionStorage)
    : DelegatingHandler, IScopedDependency
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await authSessionStorage
            .Get<string>(AuthSessionKeys.Token, cancellationToken)
            .ConfigureAwait(false);

        var userId = await authSessionStorage
            .Get<string>(AuthSessionKeys.UserId, cancellationToken)
            .ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Add("Authorization", $"Bearer {token}");
        }

        if (!string.IsNullOrWhiteSpace(userId))
        {
            request.Headers.Add("user_id", userId);
        }

        request.Headers.Add("Accept", "application/json");

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
