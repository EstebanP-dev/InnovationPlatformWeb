using Modules.Identity.Infrastructure.AuthState;

namespace Modules.Identity.Infrastructure.ServiceInstallers;

internal sealed class IdentityServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection? services, IConfiguration? configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>()
            .AddCascadingAuthenticationState()
            .AddAuthorizationCore();
    }
}
