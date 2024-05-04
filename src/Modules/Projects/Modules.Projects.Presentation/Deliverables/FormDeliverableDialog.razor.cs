using Microsoft.AspNetCore.Components.Forms;
using MudBlazor.Extensions;

namespace Modules.Projects.Presentation.Deliverables;

public partial class FormDeliverableDialog
{
    private readonly string[] _allowedExtensions = [".pdf", ".doc", ".docx"];
    private Guid inputFileId = Guid.NewGuid();

    [CascadingParameter]
    private MudDialogInstance? MudDialog { get; init; }

    [Parameter]
    public ProjectDeliverableViewModel Data { get; set; } = new();

    [Inject]
    private ISnackbar? Snackbar { get; init; }

    private void OnCancel()
    {
        MudDialog?.Cancel();
    }

    private void OnSave()
    {
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

            inputFileId = Guid.NewGuid();

            return;
        }

        if (file.Size > 4_000_000)
        {
            Snackbar?.Add(
                "El archivo es demasiado grande, el tamaño máximo permitido es de 4MB.",
                Severity.Error);

            inputFileId = Guid.NewGuid();
            return;
        }

        Data.File = e.File;
    }
}
