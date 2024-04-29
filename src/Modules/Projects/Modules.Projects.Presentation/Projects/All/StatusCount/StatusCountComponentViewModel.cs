using Modules.Projects.Application.Enumerations;
using Modules.Projects.Application.GetTotalStatusCountByUser;

namespace Modules.Projects.Presentation.Projects.All.StatusCount;

public sealed record StatusCountComponentViewModel(
    int Value,
    ProjectStatusEnumeration? Enumeration,
    DateTime? LastCreated)
{

    public static IEnumerable<StatusCountComponentViewModel> FromResponse(
        GetTotalStatusCountByUserResponse? response)
    {
        ArgumentNullException.ThrowIfNull(response);

        var lastestProject = response.Status.MaxBy(x => x.LastCreated);

        ArgumentNullException.ThrowIfNull(lastestProject);

        var totalStatus = new StatusCountComponentViewModel(
            response.Total,
            ProjectStatusEnumeration.Total,
            lastestProject.LastCreated);

        var status = response.Status.Select(FromStatusResponse);

        return [ totalStatus, ..status ];
    }

    private static StatusCountComponentViewModel FromStatusResponse(
        StatusResponse? statusResponse)
    {
        ArgumentNullException.ThrowIfNull(statusResponse);

        return new StatusCountComponentViewModel(
            statusResponse.Total,
            ProjectStatusEnumeration.FromName(statusResponse.Name),
            statusResponse.LastCreated);
    }
}
