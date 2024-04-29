namespace Modules.Identity.Infrastructure.ServiceInstallers;

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
