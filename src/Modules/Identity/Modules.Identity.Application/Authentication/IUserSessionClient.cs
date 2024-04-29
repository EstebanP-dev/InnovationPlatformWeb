using Modules.Identity.Domain.User;
using SharedKernel.Abstraction.IoC;

namespace Modules.Identity.Application.Authentication;

public interface IUserSessionClient : ITransientDependency
{
    Task UpdateAuthenticationStateAsync(UserSession? userSession, CancellationToken cancellationToken = default);
}
