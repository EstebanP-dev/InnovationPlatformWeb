using Modules.Projects.Presentation.Deliverables;

namespace Modules.Projects.Presentation.ProjectMembers;

public sealed partial class MemberAutocomplete
{
    private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();

    [Inject]
    public IDialogService? DialogService { get; init; }

    [Parameter]
    public int MaximumMembers { get; set; }

    [Parameter]
    public string? AddButtonTitle { get; set; }

    [Parameter]
#pragma warning disable CA2227
    public ProjectMembersCollection Data { get; set; } = [];

    [Parameter]
    public ProjectMembersCollection SelectedItems { get; set; } = [];
#pragma warning restore CA2227

    [Parameter]
    public EventCallback<ProjectMembersCollection> SelectedItemsChanged { get; set; }

    [Parameter]
    public bool FromCreate { get; set; } = true;

    private bool IsAddButtonDisabled => !FromCreate || SelectedItems.ToArray().Length == MaximumMembers;

    private void OpenDialog()
    {
        ArgumentNullException.ThrowIfNull(DialogService);

        var parameters = new DialogParameters<PickUserDialog>
        {
            {
                x => x.Data,
                Data
                    .Where(x => !SelectedItems.Contains(x))
            },
            {
                x => x.MaximumValuesHasBeenPicked,
                IsAddButtonDisabled
            }
        };

        _dispatcher.InvokeAsync(async () =>
        {
            var dialog = await DialogService
                .ShowAsync<PickUserDialog>("Choose a member", parameters)
                .ConfigureAwait(false);

            var result = await dialog.Result.ConfigureAwait(false);

            if (!result.Canceled)
            {
                var data = (ProjectMemberViewModel)result.Data;

                SelectedItems.Add(data);

                await SelectedItemsChanged
                    .InvokeAsync(SelectedItems)
                    .ConfigureAwait(false);
            }
        });
    }

    private void RemoveItem(ProjectMemberViewModel item)
    {
        SelectedItems.Remove(item);

        _dispatcher.InvokeAsync(async () =>
        {
            await SelectedItemsChanged
                .InvokeAsync(SelectedItems)
                .ConfigureAwait(false);
        });
    }
}
