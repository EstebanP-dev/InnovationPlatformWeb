namespace Modules.Identity.Application.Authentication.LogIn;

internal sealed record UserLoggedEvent(string? Token)
    : ApplicationEvent(Guid.NewGuid());
