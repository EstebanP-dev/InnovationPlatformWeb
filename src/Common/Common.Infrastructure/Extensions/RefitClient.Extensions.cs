using Common.Infrastructure.DelegatingHandlers;
using Refit;

namespace Common.Infrastructure.Extensions;

public static class RefitClientExtensions
{
    public static IHttpClientBuilder AddAppHttpClient<TClient, TOptions>(
            this IServiceCollection services,
            Action<TOptions,HttpClient> configureClient)
        where TClient : class
        where TOptions : class
    {
        return services
            .AddClient<TClient>()
            .AddConfiguration(configureClient)
            .AddDelegatingHandlers()
            // .AddPrimaryMessageHandler()
            .AddLifetime();
    }

    private static IHttpClientBuilder AddClient<T>(
        this IServiceCollection builder)
        where T : class
    {
        return builder
            .AddRefitClient<T>(new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                })
            });
    }

    private static IHttpClientBuilder AddDelegatingHandlers(
        this IHttpClientBuilder builder)
    {
        return builder
            .AddHttpMessageHandler<LoggingDelegatingHandler>()
            .AddHttpMessageHandler<RetryDelegatingHandler>()
            .AddHttpMessageHandler<HeaderDelegatingHandler>();
    }

    private static IHttpClientBuilder AddConfiguration<TOptions>(
        this IHttpClientBuilder builder,
        Action<TOptions,HttpClient> configureClient)
        where TOptions : class
    {
        return builder
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetService<IOptions<TOptions>>();

                ArgumentNullException.ThrowIfNull(options, nameof(options));

                var settings = options.Value;

                configureClient(settings, client);
            });
    }

    private static IHttpClientBuilder AddLifetime(
        this IHttpClientBuilder builder)
    {
        return builder
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
    }
}
