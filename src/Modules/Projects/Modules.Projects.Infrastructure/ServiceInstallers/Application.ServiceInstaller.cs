using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Configuration;

namespace Modules.Projects.Infrastructure.ServiceInstallers;

internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection? services, IConfiguration? configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
            });
    }
}
