namespace Modules.Projects.Presentation.Projects.Form.Create;

public sealed partial class Index
{
    private readonly string _deliverableFolderIdentifier = Guid.NewGuid().ToString();
    private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();
    private readonly FormViewModel _viewModel = new();
    private bool _isBusy;

    [Inject]
    private NavigationManager? NavigationManager { get; init; }

    [Inject]
    private ISender? Sender { get; init; }

    [Inject]
    private ISnackbar? Snackbar { get; init; }

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

        var requestResult = FormViewModel.ToCreateRequest(_viewModel, _deliverableFolderIdentifier);

        if (requestResult.IsFailure)
        {
            Snackbar.Add(requestResult.FirstError.Message, Severity.Error);
            _isBusy = false;
            return;
        }

        var request = requestResult.Value;

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
