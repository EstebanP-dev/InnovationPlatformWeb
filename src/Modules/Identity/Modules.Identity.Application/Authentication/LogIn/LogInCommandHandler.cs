namespace Modules.Identity.Application.Authentication.LogIn;

internal sealed class LogInCommandHandler(
    IAuthenticationClient authenticationClient,
    IAuthSessionStorage authSessionStorage,
    IPublisher publisher)
    : ICommandHandler<LogInCommand, Success>
{
    public async Task<Result<Success>> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var logInRequest = LogInCommand.ToRequest(request);

        var result = await authenticationClient
            .LogInAsync(logInRequest)
            .ConfigureAwait(false);

        if (!result.IsSuccess())
        {
            var errors = result.MapErrors();

            return Result.Failure<Success>(errors);
        }

        var resultValue = result.Value;

        if (resultValue is null)
        {
            return Result.Failure<Success>(LogInErrors.UnhandledRequest);
        }

        var token = resultValue.Token;

        if (string.IsNullOrWhiteSpace(token))
        {
            return Result.Failure<Success>(LogInErrors.UnhandledRequest);
        }

        await authSessionStorage
            .Save(AuthSessionKeys.Token, token, cancellationToken)
            .ConfigureAwait(false);

        await publisher
            .Publish(new UserLoggedEvent(token), cancellationToken)
            .ConfigureAwait(false);

        return Result.Success(Results.Success);
    }
}
