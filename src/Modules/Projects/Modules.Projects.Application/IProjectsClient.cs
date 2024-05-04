using Common.Application.Responses;
using Modules.Projects.Application.Assessors.GetAssessors;
using Modules.Projects.Application.Authors.GetAuthors;
using Modules.Projects.Application.Projects.CreateProject;
using Modules.Projects.Application.Projects.GetProjects;
using Modules.Projects.Application.Projects.GetTotalStatusCountByUser;
using Modules.Projects.Application.Types.GetProjectTypes;
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

    [Multipart]
    [Post("/")]
    Task<BaseResponse> CreateProjectAsync([Body] MultipartFormDataContent request);
}
