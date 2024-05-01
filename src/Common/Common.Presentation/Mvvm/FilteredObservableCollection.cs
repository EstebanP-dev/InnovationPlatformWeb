namespace Common.Presentation.Mvvm;

public abstract class FilteredObservableCollection<T>(
        Predicate<T>? criteria = null,
        Func<T, string, bool>? searchCriteria = null)
    : ExtendedObservableCollection<T>()
{
    private readonly Predicate<T> _criteria = criteria ?? (_ => true);
    private readonly Func<T, string, bool> _searchCriteria = searchCriteria ?? ((_, _) => true);
    private bool _isAlreadyFiltered;
    private T[]? _copyData;

    public override void AddRangeAndClear(IEnumerable<T>? collection)
    {
        _isAlreadyFiltered = false;
        base.AddRangeAndClear(collection);
    }

    public virtual void FilteredFromText(string text)
    {
        if (!_isAlreadyFiltered)
        {
            _copyData = new T[Count];
            CopyTo(_copyData, 0);
            _isAlreadyFiltered = true;
        }

        ArgumentNullException.ThrowIfNull(_copyData);

        ClearItems();

        var filteredItems = _copyData
            .Where(item =>
                _criteria(item) &&
                _searchCriteria(item, text))
            .ToArray();

        AddRange(filteredItems);
    }
}
