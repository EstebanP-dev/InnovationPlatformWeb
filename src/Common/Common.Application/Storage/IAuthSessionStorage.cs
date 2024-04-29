namespace Common.Application.Storage;

public interface IAuthSessionStorage : IScopedDependency
{
    Task Save<T>(string key, T value, CancellationToken cancellationToken = default) where T : class;

    Task<T?> Get<T>(string key, CancellationToken cancellationToken = default) where T : class;

    Task Remove(string key, CancellationToken cancellationToken = default);
}
