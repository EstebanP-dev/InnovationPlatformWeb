using System.Reflection;

namespace Modules.Projects.Application;

public sealed class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
