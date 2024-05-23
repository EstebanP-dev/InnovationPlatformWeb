using SharedKernel.Primitives;

namespace Common.Domain.Errors;

public sealed class GeneralErrors
{
    public static readonly Error BusyUi = Error.Custom(
        "600",
        message: "Prueba de nuevo más tarde.");

    public static readonly Error UnhandledRequest = Error.Unexpected(
        message: "An error occurred while processing the request.");
}
