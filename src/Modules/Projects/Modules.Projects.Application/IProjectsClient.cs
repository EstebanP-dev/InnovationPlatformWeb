using Common.Application.Responses;
using Modules.Projects.Application.Assessors.GetAssessors;
using Modules.Projects.Application.Authors.GetAuthors;
using Modules.Projects.Application.Deliverables.ChangeStatus;
using Modules.Projects.Application.DeliverableTypes.GetDeliverableTypes;
using Modules.Projects.Application.Projects.CreateProject;
using Modules.Projects.Application.Projects.GetProject;
using Modules.Projects.Application.Projects.GetProjects;
using Modules.Projects.Application.Projects.GetTotalStatusCountByUser;
using Modules.Projects.Application.Projects.UpdateProject;
using Modules.Projects.Application.Types.GetProjectTypes;
using Refit;

namespace Modules.Projects.Application;

public interface IProjectsClient
{
    [Get("/status/")]
    Task<BaseResponse<GetTotalStatusCountByUserResponse>> GetTotalStateCountByUserAsync();

    [Get("/")]
    Task<BaseResponse<IEnumerable<GetProjectsResponse>>> GetProjectsAsync();

    [Get("/{project_id}/")]
    Task<BaseResponse<GetProjectResponse>> GetProjectAsync([AliasAs("project_id")] string projectId);

    [Delete("/{project_id}/")]
    Task<BaseResponse> DeleteProjectAsync([AliasAs("project_id")] string projectId);

    [Delete("/{project_id}/force")]
    Task<BaseResponse> DeleteForceProjectAsync([AliasAs("project_id")] string projectId);

    [Get("/assessors/")]
    Task<BaseResponse<IEnumerable<GetAssessorsResponse>>> GetAssessorsAsync();

    [Get("/authors/")]
    Task<BaseResponse<IEnumerable<GetAuthorsResponse>>> GetAuthorsAsync();

    [Get("/types/")]
    Task<BaseResponse<IEnumerable<GetProjectTypesResponse>>> GetProjectTypesAsync();

    [Get("/deliverable_types/")]
    Task<BaseResponse<IEnumerable<GetDeliverableTypesResponse>>> GetDeliverableTypesAsync();

    [Post("/")]
    Task<BaseResponse<string>> CreateProjectAsync(CreateProjectRequest request);

    [Put("/{project_id}/")]
    Task<BaseResponse> UpdateProjectAsync([AliasAs("project_id")] string projectId, [Body] UpdateProjectRequest request);

    [Post("/{project_id}/deliverables/")]
    Task<BaseResponse> CreateDeliverablesAsync([AliasAs("project_id")] string projectId, [Body] CreateProjectDeliverableRequest request);

    [Post("/{project_id}/deliverables/{deliverable_id}/changeStatus")]
    Task<BaseResponse> ChangeDeliverableStatusAsync([AliasAs("project_id")] string projectId, [AliasAs("deliverable_id")] string deliverableId, [Body] ChangeStatusRequest request);
}
