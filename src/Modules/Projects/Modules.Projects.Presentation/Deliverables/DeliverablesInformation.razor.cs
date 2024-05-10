using Modules.Projects.Application.DeliverableTypes.GetDeliverableTypes;

namespace Modules.Projects.Presentation.Deliverables;

public sealed partial class DeliverablesInformation
{
    private IEnumerable<GetDeliverableTypesResponse> _deliverableTypes = [];
    private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();

    [Inject]
    private IDialogService? DialogService { get; init; }

    [Inject]
    private ISender? Sender { get; init; }

    [Inject]
    private ISnackbar? Snackbar { get; init; }

    [Parameter]
#pragma warning disable CA2227
    public ProjectDeliverablesCollection Deliverables { get; set; } = [];
#pragma warning restore CA2227

    [Parameter]
    public EventCallback<ProjectDeliverablesCollection> DeliverablesChanged { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnLoad();
    }

    private void OnLoad()
    {
        ArgumentNullException.ThrowIfNull(Sender);

        _dispatcher.InvokeAsync(async () =>
        {
            var result = await Sender
                .Send(new GetDeliverableTypesQuery())
                .ConfigureAwait(false);

            if (result.IsSuccess)
            {
                _deliverableTypes = result.Value;
            }
        });
    }

    private void OpenDialog(ProjectDeliverableViewModel? project = null)
    {
        ArgumentNullException.ThrowIfNull(DialogService);
        ArgumentNullException.ThrowIfNull(Snackbar);

        if (!_deliverableTypes.Any())
        {
            Snackbar.Add("No se han encontrado tipos de entregables, por favor intente más tarde.", Severity.Error);
            return;
        }

        var parameters = new DialogParameters
        {
            { "Data", project ?? new ProjectDeliverableViewModel() },
            { "DeliverableTypes", _deliverableTypes }
        };

        _dispatcher.InvokeAsync(async () =>
        {
            var dialog = await DialogService
                .ShowAsync<FormDeliverableDialog>("Crear un entregable", parameters)
                .ConfigureAwait(false);

            var result = await dialog.Result.ConfigureAwait(false);

            if (!result.Canceled)
            {
                var deliverable = (ProjectDeliverableViewModel)result.Data;

                var currentDeliverable = Deliverables.FirstOrDefault(x => x.Id == deliverable.Id);

                if (currentDeliverable is not null)
                {
                    Deliverables.Remove(currentDeliverable);
                }

                Deliverables.Add(deliverable);

                await DeliverablesChanged
                    .InvokeAsync(Deliverables)
                    .ConfigureAwait(false);
            }
        });
    }

    private void RemoveItem(ProjectDeliverableViewModel item)
    {
        Deliverables.Remove(item);

        _dispatcher.InvokeAsync(async () =>
        {
            await DeliverablesChanged
                .InvokeAsync(Deliverables)
                .ConfigureAwait(false);
        });
    }
}
