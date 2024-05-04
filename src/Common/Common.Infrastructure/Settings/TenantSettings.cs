namespace Common.Infrastructure.Settings;

public sealed class TenantSettings
{
    [Required]
    public string? UsbTenant { get; set; }
}
