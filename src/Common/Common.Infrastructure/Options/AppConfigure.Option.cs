namespace Common.Infrastructure.Options;

public sealed class AppConfigureOption<T>(IConfiguration configuration)
    : IConfigureOptions<T>
    where T : class
{
    public void Configure(T? options)
    {
        ArgumentNullException.ThrowIfNull(options);

        configuration
            .GetSection(options.GetType().Name)
            .Bind(options);
    }
}
