namespace Common.Presentation.Components;

public partial class TextBox
{
    private const string DefaultCssClasses = "bg-transparent px-1.5 py-1 2xl:py-3 border border-gray-300 placeholder-gray-600 placeholder text-gray-900 outline-none text-sm focus:ring-2 ring-blue-300";

    [Parameter]
    public string? Type { get; set; }

    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public string? Label { get; set; }

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

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public string? Error { get; set; }

    [Parameter]
    public string? Reference { get; set; }

    private string? _value;

    [Parameter]
#pragma warning disable BL0007
    public string? Value
#pragma warning restore BL0007
    {
        get => _value;
        set
        {
            if (_value == value)
            {
                return;
            }

            _value = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public bool Required { get; set; }
}
