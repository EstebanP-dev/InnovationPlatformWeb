namespace Modules.Projects.Presentation.Components;

public partial class Card
{
    [Parameter]
    public int Count { get; set; }

    [Parameter]
    public string? Icon { get; set; }

    [Parameter]
    public DateTime? CreatedAt { get; set; }

    [Parameter]
    public string? IconBackground { get; set; }

    private string IconClass => $"mdi mdi-{Icon}";

    private string IconContainerClass => $"w-10 h-10 rounded-full flex items-center justify-center text-white {IconBackground}";

    [Parameter]
    public string? Title { get; set; }

    private string LastestTitle()
    {
        if (CreatedAt is null || Count == 0)
        {
            return "--.--";
        }

        var now = DateTime.UtcNow;

        var span = now - CreatedAt.Value;

        var title = span switch
        {
            { TotalSeconds: < 60 } => $"Hace {Math.Round(span.TotalSeconds)} segundos",
            { TotalMinutes: < 60 } => $"Hace {Math.Round(span.TotalMinutes)} minutos",
            { TotalHours: < 24 } => $"Hace {Math.Round(span.TotalHours)} horas",
            { TotalDays: < 30 } => $"Hace {Math.Round(span.TotalDays)} días",
            { TotalDays: < 365 } => $"Hace {Math.Round(span.TotalDays / 30)} meses",
            _ => $"Hace {Math.Round(span.TotalDays / 365)} años"
        };

        return title;
    }
}
