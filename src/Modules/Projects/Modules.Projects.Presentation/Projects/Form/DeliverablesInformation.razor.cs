using Modules.Projects.Presentation.Deliverables;

namespace Modules.Projects.Presentation.Projects.Form;

public sealed partial class DeliverablesInformation
{
    private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();

    [Inject]
    public IDialogService? DialogService { get; init; }

    [Parameter]
#pragma warning disable CA2227
    public ProjectDeliverablesCollection Deliverables { get; set; } = [];
#pragma warning restore CA2227

    [Parameter]
    public EventCallback<ProjectDeliverablesCollection> DeliverablesChanged { get; set; }

    private void OpenDialog()
    {
        ArgumentNullException.ThrowIfNull(DialogService);

        _dispatcher.InvokeAsync(async () =>
        {
            var dialog = await DialogService
                .ShowAsync<FormDeliverableDialog>("Crear un entregable")
                .ConfigureAwait(false);

            var result = await dialog.Result.ConfigureAwait(false);

            if (!result.Canceled)
            {
                var deliverable = (ProjectDeliverableViewModel)result.Data;
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
