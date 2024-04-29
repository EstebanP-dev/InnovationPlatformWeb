using Blazored.SessionStorage;
using Common.Application.Storage;

namespace Common.Infrastructure.Storage;

public sealed class AuthSessionStorage(ISessionStorageService sessionStorageService)
    : IAuthSessionStorage
{
    public async Task Save<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        await sessionStorageService
            .SetItemAsync(key, value, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<T?> Get<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        return await sessionStorageService
            .GetItemAsync<T>(key, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task Remove(string key, CancellationToken cancellationToken = default)
    {
        await sessionStorageService
            .RemoveItemAsync(key, cancellationToken)
            .ConfigureAwait(false);
    }
}
