namespace Modules.Identity.Application.Authentication.LogIn;

public sealed record LogInCommand(
    string? UserName,
    string? Password)
    : ICommand<Success>
{
    internal static LogInRequest ToRequest(LogInCommand command)
    {
        return new LogInRequest
        {
            UserName = command.UserName,
            Password = command.Password
        };
    }
}
