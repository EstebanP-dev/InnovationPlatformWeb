namespace Modules.Projects.Application.Enumerations;

public sealed class ProjectTypeEnumeration(int value, string name, string icon)
    : SmartEnumeration<ProjectTypeEnumeration>(value, name)
{
    public string Icon => icon;

    public static readonly ProjectTypeEnumeration Degree = new(1, "Grado", "certificate");
    public static readonly ProjectTypeEnumeration Investigation = new(2, "Investigación", "glasses");
    public static readonly ProjectTypeEnumeration Faculty = new(3, "Facultad", "office-building");
}
