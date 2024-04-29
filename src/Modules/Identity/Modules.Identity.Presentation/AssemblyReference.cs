using System.Reflection;

namespace Modules.Identity.Presentation;

public sealed class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
