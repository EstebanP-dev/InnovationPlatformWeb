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

            _viewModel = FormViewModel.FromGetProjectResponse(project);

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

    private void HandleValidSubmit(bool ableToContinue = false)
    {
        ArgumentNullException.ThrowIfNull(NavigationManager);
        ArgumentNullException.ThrowIfNull(Sender);
        ArgumentNullException.ThrowIfNull(Snackbar);

        if (!ableToContinue)
        {
            return;
        }

        if (_isBusy)
        {
            return;
        }

        if (!_viewModel.IsValid())
        {
            Snackbar.Add(_viewModel.FirstError.Message, Severity.Error);
            return;
        }

        _isBusy = true;

        var request = FormViewModel.ToCreateRequest(_viewModel, out var message);

        if (request is null)
        {
            Snackbar.Add(message, Severity.Error);
            _isBusy = false;
            return;
        }

        _dispatcher.InvokeAsync(async () =>
        {
            var result = await Sender
                .Send(request)
                .ConfigureAwait(false);

            _isBusy = false;

            if (result.IsFailure)
            {
                Snackbar.Add(result.FirstError.Message, Severity.Error);
                return;
            }

            Snackbar.Add("Proyecto creado correctamente.", Severity.Success);
            NavigationManager.NavigateTo("/projects");
        });

        _isBusy = false;
    }
}
