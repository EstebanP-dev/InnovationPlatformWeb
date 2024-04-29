namespace SharedKernel.Exceptions;

#pragma warning disable CA1032
public sealed class InvalidErrorValueObjectHandlerException()
#pragma warning restore CA1032
    : BaseException(
    "If the result is success, the error has to be 'None' and if the result is not success, the error didn't have to be 'None'. Nevertheless, it isn't complied with this rule.",
    null);
