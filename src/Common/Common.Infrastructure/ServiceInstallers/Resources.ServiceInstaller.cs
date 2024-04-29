using System.Globalization;

namespace Common.Infrastructure.ServiceInstallers;

internal sealed class ResourcesServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection? services, IConfiguration? configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddLocalizationManager(options =>
        {
            options.IsManageExceptions = true;
            options.CurrentCulture = new CultureInfo("es-CO");
        });
    }
}
