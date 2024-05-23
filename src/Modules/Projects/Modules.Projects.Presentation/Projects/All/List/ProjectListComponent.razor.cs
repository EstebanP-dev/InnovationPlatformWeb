using Modules.Projects.Application.Projects.GetProjects;

namespace Modules.Projects.Presentation.Projects.All.List;

public sealed partial class ProjectListComponent
{
    private readonly ProjectListCollection _projects = [];

    [Inject]
    public ISender? Sender { get; init; }

    private string? SearchText { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync()
            .ConfigureAwait(false);

        await LoadProjects(true)
            .ConfigureAwait(false);
    }

    private async Task LoadProjects(bool forceRefresh = false)
    {
        ArgumentNullException.ThrowIfNull(Sender);

        if (!forceRefresh && _projects.Any())
        {
            return;
        }

        var response = await Sender
            .Send(new GetProjectsQuery())
            .ConfigureAwait(false);

        if (response.IsSuccess)
        {
            var projects = ProjectListViewModel.FromResponse(response.Value);

            _projects.Clear();
            _projects.AddRange(projects);
        }
    }
}
