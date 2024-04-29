using System.Text.Json.Serialization;

namespace Modules.Identity.Application.Authentication.LogIn;

public sealed class LogInResponse
{
    [JsonPropertyName("access_token")]
    public string? Token { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
}
