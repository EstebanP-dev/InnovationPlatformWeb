using Modules.Identity.Application.Authentication.LogIn;

namespace Modules.Identity.Application.Authentication;

public interface IAuthenticationClient
{
    [Post("/token")]
    Task<BaseResponse<LogInResponse>> LogInAsync([Body] LogInRequest request);

    [Get("/token/current")]
    Task<BaseResponse<CurrentUserResponse>> GetCurrentUser();
}
