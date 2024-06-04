namespace Modules.Identity.Application.Authentication.LogOut
{
    internal sealed class LogOutCommandHandler(IAuthSessionStorage sessionStorage)
        : ICommandHandler<LogOutCommand, Success>
    {
        public async Task<Result<Success>> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            Task[] tasks =
            [
                sessionStorage.Remove(AuthSessionKeys.UserSession, cancellationToken),
                sessionStorage.Remove(AuthSessionKeys.Token, cancellationToken),
                sessionStorage.Remove(AuthSessionKeys.UserId, cancellationToken)
            ];

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return Result.Success(new Success());
        }
    }
}
