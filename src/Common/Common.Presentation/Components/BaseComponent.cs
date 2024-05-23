namespace Common.Presentation.Components;

public abstract class BaseComponent : ComponentBase
{
    private bool _isBusy;

    protected bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy == value)
            {
                return;
            }

            _isBusy = value;
            StateHasChanged();
        }
    }
}
