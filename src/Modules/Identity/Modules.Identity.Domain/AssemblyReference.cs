using System.Reflection;

namespace Modules.Identity.Domain;

public sealed class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
