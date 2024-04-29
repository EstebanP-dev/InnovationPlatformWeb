namespace Modules.Projects.Presentation.Projects.All.List;

public sealed class ProjectListCollection
    : ExtendedObservableCollection<ProjectListViewModel>
{
    private bool _isAlreadyFiltered;
    private ProjectListViewModel[]? _copyData;

    public override void AddRangeAndClear(IEnumerable<ProjectListViewModel>? collection)
    {
        _isAlreadyFiltered = false;
        base.AddRangeAndClear(collection);
    }

    internal void FilteredFromText(string text)
    {
        if (!_isAlreadyFiltered)
        {
            _copyData = new ProjectListViewModel[Count];
            CopyTo(_copyData, 0);
            _isAlreadyFiltered = true;
        }

        ArgumentNullException.ThrowIfNull(_copyData);

        ClearItems();

        var filteredItems = _copyData.Where(item =>
            item.Title.Contains(text, StringComparison.OrdinalIgnoreCase) ||
            item.Description.Contains(text, StringComparison.OrdinalIgnoreCase) ||
            (item.Status?.Name ?? "").Contains(text, StringComparison.OrdinalIgnoreCase) ||
            (item.Type?.Name ?? "").Contains(text, StringComparison.OrdinalIgnoreCase) ||
            item.Assessor.FullName.Contains(text, StringComparison.OrdinalIgnoreCase) ||
            item.Authors.Any(x => x.FullName.Contains(text, StringComparison.OrdinalIgnoreCase)) ||
            (item.CreatedAt is not null && item.CreatedAt.ToString()!.Contains(text, StringComparison.OrdinalIgnoreCase))
        ).ToArray();

        AddRange(filteredItems);
    }
}
