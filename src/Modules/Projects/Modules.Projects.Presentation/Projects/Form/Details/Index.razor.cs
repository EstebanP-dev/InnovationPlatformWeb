using Modules.Projects.Application.Projects.DeleteProject;
using Modules.Projects.Application.Projects.GetProject;

namespace Modules.Projects.Presentation.Projects.Form.Details;

public sealed partial class Index
{
    private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();
    private FormViewModel _viewModel = new();
    private bool _isBusy;

    [Inject]
    private NavigationManager? NavigationManager { get; init; }

    [Inject]
    private ISender? Sender { get; init; }

    [Inject]
    private ISnackbar? Snackbar { get; init; }

    [Parameter]
    public string? Id { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadData();
    }

    private void LoadData()
    {
        ArgumentNullException.ThrowIfNull(Sender);
        ArgumentNullException.ThrowIfNull(Snackbar);

        if (_isBusy)
        {
            return;
        }

        _dispatcher.InvokeAsync(async () =>
        {
            _isBusy = true;

            var result = await Sender
                .Send(new GetProjectQuery(Id ?? string.Empty))
                .ConfigureAwait(false);

            if (result.IsFailure)
            {
                Snackbar.Add(result.FirstError.Message, Severity.Error);
                return;
            }

            var project = result.Value;

            var viewModelResult = FormViewModel.FromGetProjectResponse(project);

            if (viewModelResult.IsFailure)
            {
                Snackbar.Add(viewModelResult.FirstError.Message, Severity.Error);
                return;
            }

            _viewModel = viewModelResult.Value;

            _isBusy = false;
            StateHasChanged();
        });
    }

    private void Delete()
    {
        ArgumentNullException.ThrowIfNull(NavigationManager);
        ArgumentNullException.ThrowIfNull(Sender);
        ArgumentNullException.ThrowIfNull(Snackbar);

        if (_isBusy)
        {
            return;
        }

        _isBusy = true;

        _dispatcher.InvokeAsync(async () =>
        {
            var result = await Sender
                .Send(new DeleteProjectCommand(Id ?? string.Empty))
                .ConfigureAwait(false);

            _isBusy = false;

            if (result.IsFailure)
            {
                Snackbar.Add(result.FirstError.Message, Severity.Error);
                return;
            }

            Snackbar.Add("Proyecto eliminado correctamente.", Severity.Success);
            NavigationManager.NavigateTo("/projects");
        });
    }
}
