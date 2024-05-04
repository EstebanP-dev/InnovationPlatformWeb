using Blazored.SessionStorage;
using Common.Infrastructure.Options;
using Common.Infrastructure.PipelineBehaviors;
using Common.Infrastructure.Settings;

namespace Common.Infrastructure.ServiceInstallers;

internal sealed class CommonServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection? services, IConfiguration? configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddBlazoredSessionStorage()
            .ConfigureOptions<AppConfigureOption<ApiSettings>>()
            .ConfigureOptions<AppConfigureOption<TenantSettings>>();

        services
            .AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(AssemblyReference.Assemblies);

                options.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
                // options.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                // options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });
    }
}
