namespace Modules.Projects.Application.Enumerations;

public sealed class DeliverableStatusEnumeration(
        int value,
        string name,
        string stringName = "",
        string backgroundColor = "")
    : SmartEnumeration<DeliverableStatusEnumeration>(value, name)
{
    public string BackgroundColor => backgroundColor;
    public string StringName => stringName;

    public static readonly DeliverableStatusEnumeration Pending = new(
        1,
        "Pending",
        "Pendiente",
        "bg-gray-600");

    public static readonly DeliverableStatusEnumeration Reviewing = new(
        2,
        "Reviewing",
        "En revisión",
        "bg-yellow-600");

    public static readonly DeliverableStatusEnumeration Approved = new(
        3,
        "Approved",
        "Aprobado",
        "bg-green-600");

    public static readonly DeliverableStatusEnumeration Rejected = new(
        4,
        "Rejected",
        "Rechazado",
        "bg-red-600");
}
