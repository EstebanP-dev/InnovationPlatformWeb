using Common.Infrastructure.Extensions;
using Common.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Projects.Application;
using SharedKernel.Configuration;

namespace Modules.Projects.Infrastructure.ServiceInstallers;

internal sealed class WebApiClientsServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection? services, IConfiguration? configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddAppHttpClient<IProjectsClient, ApiSettings>((options, client) =>
            {
                ClientConfigurationExtensions
                    .AddMonolithConfiguration(options, client, "projects");
            });
    }
}
