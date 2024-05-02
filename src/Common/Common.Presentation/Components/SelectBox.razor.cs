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

    [Parameter]
    public TValue? Value { get; set; }

    [Parameter]
    public EventCallback<TValue?> ValueChanged { get; set; }

    private void OnValueChanged(ChangeEventArgs e)
    {
        if (e.Value is not TValue value)
        {
            return;
        }

        Value = value;
        ValueChanged.InvokeAsync(value);
    }
}
