using Modules.Projects.Application.Enumerations;

namespace Modules.Projects.Presentation.Projects.All.StatusCount;

public sealed class GetTotalStatusCountByUserCollection
    : ExtendedObservableCollection<StatusCountComponentViewModel>
{
    private readonly IEnumerable<StatusCountComponentViewModel> _defaultValue =
    [
        ..ProjectStatusEnumeration
            .GetEnumerations()
            .Select(x => new StatusCountComponentViewModel(0, x, DateTime.UtcNow))
    ];

    public GetTotalStatusCountByUserCollection()
    {
        AddRange(_defaultValue);
    }

    public void AddOnlyMatchingValues(IEnumerable<StatusCountComponentViewModel>? collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        var defaultValuesCopy = _defaultValue.ToList();

        foreach (var incomingItem in collection)
        {
            var index = defaultValuesCopy.FindIndex(x => x.Enumeration?.Name == incomingItem.Enumeration?.Name);
            if (index != -1)
            {
                defaultValuesCopy[index] = incomingItem;
            }
        }

        Clear();
        AddRange(defaultValuesCopy);
    }
};
