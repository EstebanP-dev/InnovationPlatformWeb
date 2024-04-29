namespace Common.Presentation.Components;

public sealed partial class SelectBox<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TValue>
    where TValue : notnull
{
    private const string DefaultCssClasses = "bg-transparent px-1.5 py-1 2xl:py-3 border border-gray-300 placeholder-gray-600 placeholder text-gray-900 outline-none text-sm focus:ring-2 ring-blue-300";

#pragma warning disable CA1823
    private FluentAutocomplete<TValue> _autocomplete = default!;
#pragma warning restore CA1823

    [Parameter]
    public int MaximumSelectedOptions { get; set; } = 5;

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public string? MaximumSelectedOptionsMessage { get; set; }

    [Parameter]
    public string? OptionHeaderLabel { get; set; }

    [Parameter]
    public string? NoResultsLabel { get; set; }

    [Parameter]
    public bool Multiple { get; set; }

    [Parameter]
    public bool Required { get; set; }

    [Parameter]
    public Func<TValue, string?>? OptionValue { get; set; }

    [Parameter]
    public Func<TValue, string?>? OptionText { get; set; }

    [Parameter]
    public RenderFragment? ChildrenFragment { get; set; }

    [Parameter]
    public RenderFragment<TValue>? OptionTemplate { get; set; }

    [Parameter]
    public RenderFragment<TValue>? SelectedOptionTemplate { get; set; }

    [Parameter]
    public IEnumerable<TValue> Options { get; set; } = [];

    private IEnumerable<TValue>? _selectedValues;

    [Parameter]
#pragma warning disable BL0007
    public IEnumerable<TValue>? SelectedValues
#pragma warning restore BL0007
    {
        get => _selectedValues;
        set
        {
            if (_selectedValues != null && _selectedValues.Equals(value))
            {
                return;
            }

            _selectedValues = value;
            SelectedValuesChanged.InvokeAsync(value);
        }
    }

    private TValue? _value;

    [Parameter]
#pragma warning disable BL0007
    public TValue? Value
#pragma warning restore BL0007
    {
        get => _value;
        set
        {
            if (_value != null && _value.Equals(value))
            {
                return;
            }

            _value = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<IEnumerable<TValue>> SelectedValuesChanged { get; set; }

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    private string _cssClass = DefaultCssClasses;

    [Parameter]
#pragma warning disable BL0007
    public string CssClass
#pragma warning restore BL0007
    {
        get => _cssClass;
        set
        {
            _cssClass = DefaultCssClasses + $" {value}";
        }
    }

    public async Task RemoveSelectedItemAsync(TValue? item)
    {
       await  _autocomplete
           .RemoveSelectedItemAsync(item)
           .ConfigureAwait(false);
    }

    private void OnSearchOptions(OptionsSearchEventArgs<TValue> e)
    {
        e.Items = Options
            .Where(option =>
                OptionText?.Invoke(option)?
                    .StartsWith(e.Text, StringComparison.OrdinalIgnoreCase) == true);
    }
}
