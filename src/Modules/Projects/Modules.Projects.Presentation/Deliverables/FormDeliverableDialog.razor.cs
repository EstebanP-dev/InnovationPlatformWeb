using Microsoft.AspNetCore.Components.Forms;
using Modules.Projects.Application.DeliverableTypes.GetDeliverableTypes;

namespace Modules.Projects.Presentation.Deliverables;

public partial class FormDeliverableDialog
{
    private string[] _allowedExtensions = [];
    private Guid _inputFileId = Guid.NewGuid();

    [CascadingParameter]
    private MudDialogInstance? MudDialog { get; init; }

    [Parameter]
    public ProjectDeliverableViewModel Data { get; set; } = new();

    [Parameter]
    public IEnumerable<GetDeliverableTypesResponse> DeliverableTypes { get; set; } = [];

    [Inject]
    private ISnackbar? Snackbar { get; init; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _allowedExtensions = DeliverableTypes.Select(x => x.Extension!).ToArray();
    }

    private void OnCancel()
    {
        MudDialog?.Cancel();
    }

    private void OnSave()
    {
        var deliverableType = DeliverableTypes
            .FirstOrDefault(x => x.Extension == Path.GetExtension(Data.File?.Name));

        if (deliverableType is null || string.IsNullOrWhiteSpace(deliverableType.Id))
        {
            Snackbar?.Add(
                "Ha sucedido un error inesperado, por favor intente más tarde.",
                Severity.Error);
            return;
        }

        Data.Type = deliverableType.Id;

        if (!Data.IsValid())
        {
            var errors = Data.ErrorsCollection.First();
            Snackbar?.Add(errors.Message, Severity.Warning);

            return;
        }

        MudDialog?.Close(DialogResult.Ok(Data));
    }

    private void OnFileChanged(InputFileChangeEventArgs e)
    {
        var file = e.File;

        var fileExtension = Path.GetExtension(file.Name);

        if (!_allowedExtensions.Contains(fileExtension))
        {
            Snackbar?.Add(
                "El archivo seleccionado no es válido, los formatos permitidos son PDF, DOC y DOCX.",
                Severity.Error);

            _inputFileId = Guid.NewGuid();

            return;
        }

        if (file.Size > 4_000_000)
        {
            Snackbar?.Add(
                "El archivo es demasiado grande, el tamaño máximo permitido es de 4MB.",
                Severity.Error);

            _inputFileId = Guid.NewGuid();
            return;
        }

        Data.File = e.File;
    }
}
