using System.Reflection;

namespace Modules.Projects.Infrastructure;

public sealed class AssemblyReference
{
    private static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    public static readonly Assembly[] Assemblies =
    [
        Assembly,
        Presentation.AssemblyReference.Assembly,
    ];
}
