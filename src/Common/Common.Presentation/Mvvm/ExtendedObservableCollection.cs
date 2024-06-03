using System.Collections.ObjectModel;

namespace Common.Presentation.Mvvm;

public abstract class ExtendedObservableCollection<T>()
    : ObservableCollection<T>()
{
    public void AddAndClear(T item)
    {
        Clear();

        Add(item);
    }

    public void AddRange(IEnumerable<T>? collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        foreach (var item in collection)
        {
            Add(item);
        }
    }

    public virtual void AddRangeAndClear(IEnumerable<T>? collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        ClearItems();
        AddRange(collection);
    }
}
