using Microsoft.AspNetCore.Components.Forms;
using Modules.Projects.Application.Deliverables.ChangeStatus;
using Modules.Projects.Application.DeliverableTypes.GetDeliverableTypes;
using Modules.Projects.Application.Enumerations;

namespace Modules.Projects.Presentation.Deliverables;

public sealed partial class DeliverablesInformation
{
    private string[] _allowedExtensions = [];
    private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();
    private readonly int _maxAllowedFiles = 5;
    private IEnumerable<GetDeliverableTypesResponse> _deliverableTypes = [];

    [Inject]
    private NavigationManager? NavigationManager { get; init; }

    [Inject]
    private ISender? Sender { get; init; }

    [Inject]
    private ISnackbar? Snackbar { get; init; }

    [Inject]
    private IJSRuntime? JsRuntime { get; init; }

    [Parameter]
    public bool FromCreate { get; set; } = true;

    [Parameter]
    public ProjectStatusEnumeration Status { get; set; } = ProjectStatusEnumeration.Pending;

    [Parameter]
#pragma warning disable CA2227
    public ProjectDeliverablesCollection Deliverables { get; set; } = [];
#pragma warning restore CA2227

    [Parameter]
    public EventCallback<ProjectDeliverablesCollection> DeliverablesChanged { get; set; }

    [Parameter]
    public string? DeliverableFolderIdentifier { get; set; }

    [Parameter]
    public string? ProjectId { get; set; }

    [Parameter]
    public EventCallback<DeliverableStatusEnumeration> OnDerivebleStatusChanged { get; set; }

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
                _allowedExtensions = _deliverableTypes.Select(x => x.Extension!).ToArray();
            }
        });
    }

    private void DownloadFile(ProjectDeliverableViewModel item)
    {
        ArgumentNullException.ThrowIfNull(Sender);
        ArgumentNullException.ThrowIfNull(Snackbar);
        ArgumentNullException.ThrowIfNull(NavigationManager);

        if (item.Status.Equals(DeliverableStatusEnumeration.Reviewing))
        {
            NavigationManager.NavigateTo(item.File ?? "#");
            _dispatcher.InvokeAsync(async () =>
            {
                await OnDerivebleStatusChanged
                    .InvokeAsync(item.Status)
                    .ConfigureAwait(false);
            });
            return;
        }

        _dispatcher.InvokeAsync(async () =>
        {
            var result = await ChangedStatus(item, DeliverableStatusEnumeration.Reviewing)
                .ConfigureAwait(false);

            if (result)
            {
                NavigationManager.NavigateTo(item.File ?? "#");
            }
        });
    }

    private void ApprovedDeliverable(ProjectDeliverableViewModel? item)
    {
        ArgumentNullException.ThrowIfNull(item);
        ArgumentNullException.ThrowIfNull(Snackbar);
        ArgumentNullException.ThrowIfNull(Sender);

        if (!item.Status.Equals(DeliverableStatusEnumeration.Reviewing))
        {
            Snackbar.Add("El archivo seleccionado no se encuentra en revisión.", Severity.Error);
            return;
        }

        _dispatcher.InvokeAsync(async () =>
        {
            await ChangedStatus(item, DeliverableStatusEnumeration.Approved)
                .ConfigureAwait(false);
        });
    }

    private void RejectedDeliverable(ProjectDeliverableViewModel? item)
    {
        ArgumentNullException.ThrowIfNull(item);
        ArgumentNullException.ThrowIfNull(Snackbar);
        ArgumentNullException.ThrowIfNull(Sender);

        if (!item.Status.Equals(DeliverableStatusEnumeration.Reviewing))
        {
            Snackbar.Add("El archivo seleccionado no se encuentra en revisión.", Severity.Error);
            return;
        }

        _dispatcher.InvokeAsync(async () =>
        {
            await ChangedStatus(item, DeliverableStatusEnumeration.Rejected)
                .ConfigureAwait(false);
        });
    }

    private async Task<bool> ChangedStatus(ProjectDeliverableViewModel item, DeliverableStatusEnumeration status)
    {
        ArgumentNullException.ThrowIfNull(Snackbar);
        ArgumentNullException.ThrowIfNull(Sender);

        if (string.IsNullOrWhiteSpace(item.Id))
        {
            Snackbar.Add("No se encontró el archivo seleccionado.", Severity.Error);
            return false;
        }

        item.Status = status;

        var result = await Sender
            .Send(new ChangeStatusCommand(item.ProjectId,
                item.Id,
                item.Status.Name))
            .ConfigureAwait(false);

        if (result.IsFailure)
        {
            Snackbar.Add(result.FirstError.Message);
            return false;
        }

        await OnDerivebleStatusChanged
            .InvokeAsync(item.Status)
            .ConfigureAwait(false);
        StateHasChanged();
        return true;
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

    private async Task OnInputFileChanged(InputFileChangeEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(JsRuntime);

        if (UnableToLoadFilesDueToPreConditionParameters())
        {
            Snackbar?.Add("Algo salió mal, por favor intente más tarde.", Severity.Error);
            return;
        }

        ArgumentNullException.ThrowIfNull(DeliverableFolderIdentifier);

        if (!FromCreate)
        {
            ArgumentNullException.ThrowIfNull(ProjectId);
        }

        foreach (var file in e.GetMultipleFiles(_maxAllowedFiles))
        {
            if (file.Size > 4_000_000)
            {
                Snackbar?.Add(
                    "El archivo es demasiado grande, el tamaño máximo permitido es de 4MB.",
                    Severity.Error);
                continue;
            }

            var fileExtension = Path.GetExtension(file.Name);

            if (!_allowedExtensions.Contains(fileExtension))
            {
                Snackbar?.Add(
                    "El archivo seleccionado no es válido, los formatos permitidos son PDF, DOC y DOCX.",
                    Severity.Error);

                continue;
            }

            var deliverableType = _deliverableTypes
                .FirstOrDefault(x => x.Extension == fileExtension);

            if (deliverableType is null || string.IsNullOrWhiteSpace(deliverableType.Id))
            {
                Snackbar?.Add(
                    "Ha sucedido un error inesperado, por favor intente más tarde.",
                    Severity.Error);
                return;
            }

            var type = deliverableType.Id;

            if (string.IsNullOrWhiteSpace(type))
            {
                Snackbar?.Add(
                    "Ha sucedido un error inesperado, por favor intente más tarde.",
                    Severity.Error);
                return;
            }

            try
            {
                using var stream = file.OpenReadStream();
                var buffer = new byte[file.Size];
                var _ = await stream.ReadAsync(buffer).ConfigureAwait(false);

                var fileReference = new FileReference(
                    DeliverableFolderIdentifier,
                    file.Name,
                    buffer,
                    file.ContentType);

                var downloadUrl = await JsRuntime
                    .InvokeAsync<string>("uploadToFirebase", fileReference)
                    .ConfigureAwait(false);

                var fileName = Path.GetFileNameWithoutExtension(file.Name);

                Deliverables.Add(new ProjectDeliverableViewModel
                {
                    ProjectId = ProjectId ?? "",
                    Name = fileName,
                    File = downloadUrl,
                    Type = type
                });
            }
#pragma warning disable CA1031
            catch (Exception ex)
#pragma warning restore CA1031
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
            }

        }
    }

    private bool UnableToLoadFilesDueToPreConditionParameters()
    {
        return (string.IsNullOrWhiteSpace(ProjectId) && !FromCreate)
            || string.IsNullOrWhiteSpace(DeliverableFolderIdentifier)
            || DeliverableFolderIdentifier
                   .Equals(Guid.Empty.ToString(),
                       StringComparison.OrdinalIgnoreCase);
    }
}
