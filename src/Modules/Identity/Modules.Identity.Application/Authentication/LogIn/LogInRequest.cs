using System.Text.Json.Serialization;

namespace Modules.Identity.Application.Authentication.LogIn;

public sealed class LogInRequest
{
    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
