using Scrutor;
using SharedKernel.Abstraction.IoC;
using SharedKernel.Configuration;

namespace Shell.Extensions;

public static class InstallServicesExtension
{
    internal static IHostApplicationBuilder InstallServices(
        this IHostApplicationBuilder? builder,
        params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder
            .Services
            .AddDependencyServices(assemblies)
            .AddServiceInstaller(builder.Configuration, assemblies);

        return builder;
    }

    private static IServiceCollection AddDependencyServices(
        this IServiceCollection? services,
        Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableTo<ITransientDependency>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsSelfWithInterfaces()
                .WithTransientLifetime());

        services
            .Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableTo<IScopedDependency>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        services
            .Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableTo<ISingletonDependency>())
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsSelfWithInterfaces()
                .WithSingletonLifetime());

        return services;
    }

    private static IServiceCollection AddServiceInstaller(
        this IServiceCollection? services,
        IConfiguration? configuration,
        IEnumerable<Assembly> assemblies)
    {
        IEnumerable<IServiceInstaller> serviceInstallers = assemblies
            .SelectMany(x => x.DefinedTypes)
            .Where(IsAssignableToType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (IServiceInstaller serviceInstaller in serviceInstallers)
        {
            serviceInstaller.Install(services, configuration);
        }

        ArgumentNullException.ThrowIfNull(services);

        return services;
    }

    private static bool IsAssignableToType<T>(TypeInfo typeInfo)
    {
        return typeof(T)
                   .IsAssignableFrom(typeInfo)
               && typeInfo is { IsInterface: false, IsAbstract: false };
    }
}
