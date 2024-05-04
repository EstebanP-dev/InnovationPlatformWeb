using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components.Forms;

namespace Modules.Projects.Presentation.Deliverables;

public sealed class ProjectDeliverableViewModel : ObservableValidator
{
    [Required]
    [StringLength(maximumLength: 255, MinimumLength = 5)]
    public string Name { get; set; } = string.Empty;

    [StringLength(maximumLength: 1000, MinimumLength = 5)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public IBrowserFile? File { get; set; }

    public bool IsValid(out IEnumerable<ValidationResult> errors)
    {
        ValidateAllProperties();

        errors = GetErrors();

        return !HasErrors;
    }
}
