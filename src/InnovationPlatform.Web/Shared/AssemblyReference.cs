using System.Reflection;

namespace InnovationPlatform.Web.Shared;

public sealed class AssemblyReference
{
    public static readonly Assembly[] ExternalAssemblies =
    [
        ..Common.Infrastructure.AssemblyReference.Assemblies,
        ..Modules.Projects.Infrastructure.AssemblyReference.Assemblies,
        ..Modules.Identity.Infrastructure.AssemblyReference.Assemblies
    ];
}
