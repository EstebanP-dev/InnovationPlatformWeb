using SharedKernel.Primitives;

namespace Common.Domain.Errors;

public sealed class GeneralErrors
{
    public static readonly Error UnhandledRequest = Error.Unexpected(
        message: "An error occurred while processing the request.");
}
