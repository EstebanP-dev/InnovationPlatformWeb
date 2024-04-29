using System.Reflection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Hosting;
using Scrutor;
using SharedKernel.Abstraction.IoC;
using SharedKernel.Configuration;

namespace InnovationPlatform.Web.Client.Extensions;

public static class InstallServicesExtension
{
    internal static WebAssemblyHostBuilder InstallServices(
        this WebAssemblyHostBuilder? builder,
        params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder
            .AddConfiguration()
            .Services
            .AddDependencyServices(assemblies)
            .AddServiceInstaller(builder.Configuration, assemblies);

        return builder;
    }

    private static WebAssemblyHostBuilder AddConfiguration(
        this WebAssemblyHostBuilder? builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // ReSharper disable once StringLiteralTypo
        const string file = @"InnovationPlatform.Web.Client.appsettings.json";

        using Stream? stream = typeof(InstallServicesExtension).Assembly.GetManifestResourceStream(file);

        ArgumentNullException.ThrowIfNull(stream);

        var configuration = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(configuration);

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
