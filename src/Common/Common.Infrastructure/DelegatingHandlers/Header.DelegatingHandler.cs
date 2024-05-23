using Common.Application.Storage;
using Common.Infrastructure.Settings;

namespace Common.Infrastructure.DelegatingHandlers;

internal sealed class HeaderDelegatingHandler(
        IAuthSessionStorage authSessionStorage,
        IOptions<TenantSettings> tenantSettings)
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

        var tenant = tenantSettings.Value.UsbTenant;

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Add("Authorization", $"Bearer {token}");
        }

        if (!string.IsNullOrWhiteSpace(userId))
        {
            request.Headers.Add("X-User-Id", userId);
        }

        if (!string.IsNullOrWhiteSpace(tenant))
        {
            request.Headers.Add("X-Tenant-Id", tenant);
        }


        request.Headers.Add("Accept", "application/json");

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
