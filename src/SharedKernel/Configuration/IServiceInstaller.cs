namespace SharedKernel.Configuration;

/// <summary>
/// Interface to install services.
/// </summary>
public interface IServiceInstaller
{
    /// <summary>
    /// Services installer.
    /// </summary>
    /// <param name="services">WebApi services.</param>
    /// <param name="configuration">WebApi configuration.</param>
    void Install(IServiceCollection? services, IConfiguration? configuration);
}
