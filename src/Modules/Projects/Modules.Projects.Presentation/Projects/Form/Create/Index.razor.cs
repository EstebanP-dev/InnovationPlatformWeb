using System.Text.Json;

namespace Modules.Projects.Presentation.Projects.Form.Create;

public sealed partial class Index
{
    private readonly CreateProjectViewModel _viewModel = new();

    [Inject]
    private ISnackbar? Snackbar { get; init; }

    private void ShowInformation()
    {
        var json = JsonSerializer.Serialize(_viewModel);

        Snackbar?.Add(json, Severity.Info);

        Console.WriteLine(json);
    }

    private void UpdateTitle(string newValue)
    {
        _viewModel.Title = newValue;
    }
}
