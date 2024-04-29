using Common.Infrastructure.Settings;

namespace Common.Infrastructure.Extensions;

public static class ClientConfigurationExtensions
{
    public static void AddMonolithConfiguration(ApiSettings? options, HttpClient? client , string schema = "")
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(options.BaseUrl);
        ArgumentNullException.ThrowIfNull(options.Version);

        var schemaSection = string.IsNullOrWhiteSpace(schema)
            ? string.Empty
            : "/" + schema;

        client.BaseAddress = new Uri(options.BaseUrl + options.Version + schemaSection);
    }
}
