using SharedKernel.Primitives;

namespace Modules.Projects.Domain.Deliverables;

public sealed class DeliverablesError
{
    public static readonly Error UnavailableToCreate = Error.Unexpected(
        message: "No se ha podido crear el entregable.");
}
