using System.Reflection;

namespace Common.Infrastructure;

public sealed class AssemblyReference
{
    private static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    public static readonly Assembly[] Assemblies =
    [
        Assembly,
    ];
}
