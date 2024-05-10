namespace Common.Presentation.Components;

public partial class TableRow
{
    [Parameter]
    public EventCallback OnClick { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private void HandleOnClick()
    {
        OnClick.InvokeAsync();
    }
}
