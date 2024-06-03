using CommunityToolkit.Mvvm.Messaging;
using Modules.Projects.Application.Enumerations;
using Modules.Projects.Application.Projects.ChangeProjectStatus;
using Modules.Projects.Application.Projects.DeleteProject;
using Modules.Projects.Application.Projects.GetProject;
using Modules.Projects.Presentation.Messages;

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

    private void Modify()
    {
        ArgumentNullException.ThrowIfNull(Sender);
        ArgumentNullException.ThrowIfNull(Snackbar);

        if (_isBusy)
        {
            return;
        }

        var requestResult = FormViewModel.ToUpdateRequest(_viewModel);

        if (requestResult.IsFailure)
        {
            Snackbar.Add(requestResult.FirstError.Message, Severity.Error);
            return;
        }

        var request = requestResult.Value;

        _isBusy = true;

        _dispatcher.InvokeAsync(async () =>
        {
            var result = await Sender
                .Send(request)
                .ConfigureAwait(false);

            _isBusy = false;
            StateHasChanged();

            if (result.IsFailure)
            {
                Snackbar.Add(result.FirstError.Message, Severity.Error);
                return;
            }

            Snackbar.Add("Proyecto modificado correctamente.", Severity.Success);
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

    private void ApproveProject()
    {
        ArgumentNullException.ThrowIfNull(Snackbar);

        if (!_viewModel.IsValid())
        {
            Snackbar.Add(_viewModel.FirstError.Message, Severity.Error);
            return;
        }

        if (!_viewModel.Deliverables.All(x => x.Status.Equals(DeliverableStatusEnumeration.Approved)))
        {
            Snackbar.Add("No se pueden aprobar proyectos con entregables pendientes.", Severity.Error);
            return;
        }

        ChangeStatus(ProjectStatusEnumeration.Completed);
    }

    private void RejectProject()
    {
        ArgumentNullException.ThrowIfNull(Snackbar);

        if (!_viewModel.IsValid())
        {
            Snackbar.Add(_viewModel.FirstError.Message, Severity.Error);
            return;
        }

        var status = ProjectStatusEnumeration.Waiting;

        WeakReferenceMessenger.Default.Send(new StatusChanged(status));

        ChangeStatus(status);
    }

    private void ChangeStatus(ProjectStatusEnumeration status)
    {
        ArgumentNullException.ThrowIfNull(Sender);
        ArgumentNullException.ThrowIfNull(Snackbar);

        if (string.IsNullOrWhiteSpace(_viewModel.Id))
        {
            Snackbar.Add("No se ha encontrado el proyecto.", Severity.Error);
            return;
        }

        InvokeAsync(async () =>
        {
            var result = await Sender
                .Send(new ChangeProjectStatusCommand(_viewModel.Id, status.Name))
                .ConfigureAwait(false);

            if (result.IsFailure)
            {
                Snackbar.Add(result.FirstError.Message, Severity.Error);
                return;
            }

            _viewModel.Status = status;

            Snackbar.Add("Estado del proyecto modificado correctamente.", Severity.Success);
            StateHasChanged();
        });
    }

    private void OnDeliverableStartReview(DeliverableStatusEnumeration deliverableStatus)
    {
        if (!_viewModel.Status.Equals(ProjectStatusEnumeration.Pending))
        {
            return;
        }

        ChangeStatus(ProjectStatusEnumeration.InProgress);
    }
}
