namespace Modules.Projects.Presentation.ProjectMembers;

public sealed partial class MemberAutocomplete
{
    private readonly ProjectMembersCollection _data = [..ProjectMemberViewModel.FromResponse()];
    private readonly ProjectMembersCollection _selectedItems = [];

    [Inject]
    public IDialogService? DialogService { get; init; }

    [Parameter]
    public int MaximumMembers { get; set; }

    [Parameter]
    public string? AddButtonTitle { get; set; }

    private bool IsAddButtonDisabled => _selectedItems.Count == MaximumMembers;

    private async Task OpenDialog()
    {
        ArgumentNullException.ThrowIfNull(DialogService);

        var parameters = new DialogParameters<PickUserDialog>
        {
            {
                x => x.Data,
                _data
                    .Where(x => !_selectedItems.Contains(x))
            },
            {
                x => x.MaximumValuesHasBeenPicked,
                IsAddButtonDisabled
            }
        };

        var dialog = await DialogService
            .ShowAsync<PickUserDialog>("Choose a member", parameters)
            .ConfigureAwait(false);

        var result = await dialog.Result.ConfigureAwait(false);

        if (!result.Canceled)
        {
            _selectedItems.Add((ProjectMemberViewModel)result.Data);
        }
    }

    private void RemoveItem(ProjectMemberViewModel item)
    {
        _selectedItems.Remove(item);
    }
}
