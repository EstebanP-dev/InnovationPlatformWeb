namespace Modules.Projects.Presentation.Projects.Form;

public sealed partial class GeneralInformationComponent
{
    private string _title = "";

    [Parameter]
#pragma warning disable BL0007
    public string Title
#pragma warning restore BL0007
    {
        get => _title;
        set
        {
            if (_title == value)
            {
                return;
            }

            _title = value;
            TitleChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<string> TitleChanged { get; set; }

    private string _description = "";

    [Parameter]
#pragma warning disable BL0007
    public string Description
#pragma warning restore BL0007
    {
        get => _description;
        set
        {
            if (_description == value)
            {
                return;
            }

            _description = value;
            DescriptionChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<string> DescriptionChanged { get; set; }

    private string _type = "";

    [Parameter]
#pragma warning disable BL0007
    public string Type
#pragma warning restore BL0007
    {
        get => _type;
        set
        {
            if (_type == value)
            {
                return;
            }

            _type = value;
            TypeChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<string> TypeChanged { get; set; }
}
