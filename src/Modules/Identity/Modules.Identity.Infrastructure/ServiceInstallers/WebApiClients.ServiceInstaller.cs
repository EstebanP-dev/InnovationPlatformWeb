using Common.Infrastructure.Extensions;
using Common.Infrastructure.Settings;
using Modules.Identity.Application.Authentication;

namespace Modules.Identity.Infrastructure.ServiceInstallers;

internal sealed class WebApiClientsServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection? services, IConfiguration? configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddAppHttpClient<IAuthenticationClient, ApiSettings>((options, settings) =>
            {
                ClientConfigurationExtensions
                    .AddMonolithConfiguration(options, settings, "auth");
            });
    }
}
