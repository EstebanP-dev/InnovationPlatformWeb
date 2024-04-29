using Common.Application.Responses;
using Modules.Projects.Application.GetProjects;
using Modules.Projects.Application.GetTotalStatusCountByUser;
using Refit;

namespace Modules.Projects.Application;

public interface IProjectsClient
{
    [Get("/status/total")]
    Task<BaseResponse<GetTotalStatusCountByUserResponse>> GetTotalStateCountByUserAsync();

    [Get("/")]
    Task<BaseResponse<IEnumerable<GetProjectsResponse>>> GetProjectsAsync();
}
