namespace Common.Infrastructure.Settings;

public sealed class ApiSettings
{
    [Required]
#pragma warning disable CA1056
    public string? BaseUrl { get; set; }
#pragma warning restore CA1056

    [Required]
    public string? Version { get; set; }
}
