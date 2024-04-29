namespace Common.Presentation.Components;

public partial class Button
{
    private const string DefaultCssClasses = "px-3 py-2 outline-none";

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

    private string? _icon;

    [Parameter]
#pragma warning disable BL0007
    public string? Icon
#pragma warning restore BL0007
    {
        get => _icon;
        set
        {
            if (_icon == value)
            {
                return;
            }

            _icon = $"mdi mdi-{value}";
        }
    }

    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }

    [Parameter]
    public string? Type { get; set; }
}
