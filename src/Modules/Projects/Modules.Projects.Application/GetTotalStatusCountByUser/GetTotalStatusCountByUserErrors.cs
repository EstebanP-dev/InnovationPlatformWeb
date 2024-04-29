using SharedKernel.Primitives;

namespace Modules.Projects.Application.GetTotalStatusCountByUser;

internal sealed class GetTotalStatusCountByUserErrors
{
    public static readonly Error UnexpectedError =
        Error.Unexpected(message: "An unexpected error occurred while getting the total status count by user.");
}
