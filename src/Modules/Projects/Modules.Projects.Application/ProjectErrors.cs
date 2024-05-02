namespace Modules.Projects.Application;

internal sealed class ProjectErrors
{
    internal static readonly Error NoAuthorsFound = Error
        .NotFound(message: "No se encontraron autores disponibles.");

    internal static readonly Error NoAssessorsFound = Error
        .NotFound(message: "No se encontraron asessores disponibles.");
}
