using System.ComponentModel.DataAnnotations;
using Common.Presentation.Files;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components.Forms;
using Modules.Projects.Application.Projects.CreateProject;
using Modules.Projects.Application.Projects.GetProject;

namespace Modules.Projects.Presentation.Deliverables;

public sealed class ProjectDeliverableViewModel : BaseViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(maximumLength: 255, MinimumLength = 5)]
    public string Name { get; set; } = string.Empty;

    [StringLength(maximumLength: 1000, MinimumLength = 5)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public IBrowserFile? File { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;

    internal static ProjectDeliverableViewModel FromGetProjectDeliverableResponse(GetProjectDeliverableResponse? response)
    {
        return new ProjectDeliverableViewModel
        {
            Id = Guid.NewGuid(),
            Name = response?.Name ?? "",
            Description = response?.Description ?? "",
            Type = response?.Type ?? ""
        };
    }

    internal static (CreateProjectDeliverableDto?, string) ToRequest(ProjectDeliverableViewModel viewModel)
    {
        if (viewModel.File is null)
        {
            return (null, "El archivo es requerido.");
        }

        var response = new CreateProjectDeliverableDto(
            Guid.NewGuid(),
            viewModel.File.Name,
            viewModel.File.ContentType,
            viewModel.Type,
            viewModel.Name,
            new FileProvider(viewModel.File),
            viewModel.Description);

        return (response, "");
    }
}
