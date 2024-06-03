namespace Common.Presentation.Components;

public sealed partial class TextArea
{
    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public bool Required { get; set; }

    [Parameter]
    public bool ReadOnly { get; set; }

    [Parameter]
    public string? Value { get; set; }

    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    private void OnValueChanged(ChangeEventArgs e)
    {
        Value = e.Value?.ToString();
        ValueChanged.InvokeAsync(Value);
    }
}
