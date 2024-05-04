using Modules.Projects.Application.Assessors.GetAssessors;
using Modules.Projects.Application.Authors.GetAuthors;
using Modules.Projects.Presentation.ProjectMembers;

namespace Modules.Projects.Presentation.Projects.Form;

public sealed partial class MembersInformation
{
    private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();
    private readonly ProjectMembersCollection _data = [];

    [Parameter]
    public string? AddButtonTitle { get; set; }

    [Parameter]
    public int MaximumMembers { get; set; } = 3;

    [Parameter]
    public bool LoadAssessors { get; set; }

    [Parameter]
#pragma warning disable CA2227
    public ProjectMembersCollection SelectedItems { get; set; } = [];
#pragma warning restore CA2227

    [Parameter]
    public EventCallback<ProjectMembersCollection> SelectedItemsChanged { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Inject]
    private ISender? Sender { get; init; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadMembers();
    }

    private void LoadMembers()
    {
        ArgumentNullException.ThrowIfNull(Sender);

        if (LoadAssessors)
        {
            _dispatcher.InvokeAsync(async () =>
            {
                var result = await Sender
                    .Send(new GetAssessorsQuery())
                    .ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    _data.AddRangeAndClear(ProjectMemberViewModel.FromAssessorsResponse(result.Value));
                }
            });
        }
        else
        {
            _dispatcher.InvokeAsync(async () =>
            {
                var result = await Sender
                    .Send(new GetAuthorsQuery())
                    .ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    _data.AddRangeAndClear(ProjectMemberViewModel.FromAuthorsResponse(result.Value));
                }
            });
        }
    }
}
