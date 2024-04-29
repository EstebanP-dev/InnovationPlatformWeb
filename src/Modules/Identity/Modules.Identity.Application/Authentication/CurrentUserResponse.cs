using System.Text.Json.Serialization;
using Modules.Identity.Domain.User;

namespace Modules.Identity.Application.Authentication;

public sealed class CurrentUserResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }

    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("roles")]
    public IEnumerable<string> Roles { get; set; } = [];

    public UserSession ToUserSession()
    {
        return new UserSession(
            Id ?? string.Empty,
            FullName ?? string.Empty,
            UserName ?? string.Empty,
            Email ?? string.Empty,
            Roles.Select(x => new UserSessionRole(Guid.NewGuid(), x)));
    }
}
