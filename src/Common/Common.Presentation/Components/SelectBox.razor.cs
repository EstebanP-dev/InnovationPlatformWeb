namespace Common.Presentation.Components;

public sealed partial class SelectBox<TValue>
    where TValue : notnull
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public bool Required { get; set; }

    private TValue? _value;

    [Parameter]
#pragma warning disable BL0007
    public TValue? Value
#pragma warning restore BL0007
    {
        get => _value;
        set
        {
            if (EqualityComparer<TValue>.Default.Equals(_value, value))
            {
                return;
            }

            _value = value;
            ValueChanged.InvokeAsync(_value);
        }
    }

    [Parameter]
    public EventCallback<TValue?> ValueChanged { get; set; }
}
