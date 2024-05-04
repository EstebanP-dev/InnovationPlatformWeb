using Modules.Projects.Application.Types.GetProjectTypes;

namespace Modules.Projects.Presentation.Types;

public sealed partial class ProjectTypesComponent
{
    private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();
    private readonly ProjectTypesCollection _data = [];

    [Parameter]
    public string? SelectedType { get; set; }

    [Parameter]
    public EventCallback<string?> SelectedTypeChanged { get; set; }

    [Inject]
    private ISender? Sender { get; init; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadData();
    }

    private void LoadData()
    {
        ArgumentNullException.ThrowIfNull(Sender);

        _dispatcher.InvokeAsync(async () =>
        {
            var result = await Sender
                .Send(new GetProjectTypesQuery())
                .ConfigureAwait(false);

            if (result.IsSuccess)
            {
                var data = result.Value;

                _data.AddRangeAndClear(ProjectTypeViewModel.FromResponse(data));
                StateHasChanged();
            }
        });
    }
}
