using System.ComponentModel.DataAnnotations;
using SharedKernel.Primitives;

namespace Common.Presentation.Mvvm;

public abstract partial class BaseViewModel : ObservableValidator
{
    [ObservableProperty]
    private ErrorsCollection _errorsCollection = [];

    public Error FirstError => ErrorsCollection.FirstOrDefault() ?? Error.None();

    public virtual bool IsValid()
    {
        ValidateAllProperties();

        var errors = GetErrors()
            .Select(x => Error.Validation(message: x.ErrorMessage ?? "Error de validación."));

        ErrorsCollection.AddRangeAndClear(errors);

        return !HasErrors;
    }
}
