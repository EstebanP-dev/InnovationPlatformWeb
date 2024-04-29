namespace Modules.Identity.Domain.User;

public sealed record UserSession(
    string? Id,
    string? FullName,
    string? UserName,
    string? Email,
    IEnumerable<UserSessionRole> Roles);
