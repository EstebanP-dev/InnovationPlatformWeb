using Common.Application.Responses;
using Modules.Projects.Application.GetAssessors;
using Modules.Projects.Application.GetAuthors;
using Modules.Projects.Application.GetProjects;
using Modules.Projects.Application.GetProjectTypes;
using Modules.Projects.Application.GetTotalStatusCountByUser;
using Refit;

namespace Modules.Projects.Application;

public interface IProjectsClient
{
    [Get("/status/total")]
    Task<BaseResponse<GetTotalStatusCountByUserResponse>> GetTotalStateCountByUserAsync();

    [Get("/")]
    Task<BaseResponse<IEnumerable<GetProjectsResponse>>> GetProjectsAsync();

    [Get("/assessors/for_creation")]
    Task<BaseResponse<IEnumerable<GetAssessorsResponse>>> GetAssessorsAsync();

    [Get("/authors/for_creation")]
    Task<BaseResponse<IEnumerable<GetAuthorsResponse>>> GetAuthorsAsync();

    [Get("/types/for_creation")]
    Task<BaseResponse<IEnumerable<GetProjectTypesResponse>>> GetProjectTypesAsync();
}
