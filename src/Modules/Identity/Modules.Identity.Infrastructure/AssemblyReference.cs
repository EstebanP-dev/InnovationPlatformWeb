using System.Reflection;

namespace Modules.Identity.Infrastructure;

public sealed class AssemblyReference
{
    private static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    public static readonly Assembly[] Assemblies =
    [
        Assembly,
        Application.AssemblyReference.Assembly,
        Domain.AssemblyReference.Assembly,
        Presentation.AssemblyReference.Assembly,
    ];
}
