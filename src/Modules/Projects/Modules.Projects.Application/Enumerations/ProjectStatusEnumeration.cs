namespace Modules.Projects.Application.Enumerations;

public sealed class ProjectStatusEnumeration(int value, string name, string backgroundColor = "", string icon = "")
    : SmartEnumeration<ProjectStatusEnumeration>(value, name)
{
    public string BackgroundColor => backgroundColor;
    public string Icon => icon;

    public static readonly ProjectStatusEnumeration Total = new(
        1,
        "Total",
        icon: "note");

    public static readonly ProjectStatusEnumeration Completed = new(
        2,
        "Completado",
        "bg-green-600",
        "progress-check");

    public static readonly ProjectStatusEnumeration InProgress = new(
        3,
        "En Progreso",
        "bg-yellow-600",
        "table-edit");

    public static readonly ProjectStatusEnumeration Waiting = new(
        4,
        "En Espera",
        "bg-blue-600",
        "pause");

    public static readonly ProjectStatusEnumeration Pending = new(
        5,
        "Pendiente",
        "bg-gray-600",
        "clock");
}
