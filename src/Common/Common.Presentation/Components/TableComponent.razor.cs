namespace Common.Presentation.Components;

public partial class TableComponent
{
    [Parameter]
    public EventCallback<string> OnSearchTextChanged { get; set; }

    [Parameter]
    public RenderFragment? TableHeaders { get; set; }

    [Parameter]
    public RenderFragment? TableBody { get; set; }

    private string _searchText = "";
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value)
            {
                return;
            }

            _searchText = value;
            OnSearchTextChanged.InvokeAsync(value);
        }
    }
}
