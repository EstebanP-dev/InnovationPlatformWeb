using System.ComponentModel.DataAnnotations;
using Common.Presentation.Files;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components.Forms;
using Modules.Projects.Application.Enumerations;
using Modules.Projects.Application.Projects.CreateProject;
using Modules.Projects.Application.Projects.GetProject;
using SharedKernel.Primitives;

namespace Modules.Projects.Presentation.Deliverables;

public sealed class  ProjectDeliverableViewModel : BaseViewModel
{
    public string? Id { get; set; } = string.Empty;

    [Required]
    [StringLength(maximumLength: 255, MinimumLength = 5)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string? File { get; set; }

    [Required]
    public string Type { get; set; } = string.Empty;

    public required string ProjectId { get; set; }

    [Required]
    public DeliverableStatusEnumeration Status { get; set; } = DeliverableStatusEnumeration.Pending;

    internal static ProjectDeliverableViewModel FromGetProjectDeliverableResponse(GetProjectDeliverableResponse? response, string projectId)
    {
        return new ProjectDeliverableViewModel
        {
            Id = response?.Id ?? "",
            Name = response?.Name ?? "",
            Type = response?.Type ?? "",
            Status = DeliverableStatusEnumeration.FromName(response?.Status ?? "") ?? DeliverableStatusEnumeration.Pending,
            File = response?.Url?.ToString() ?? "",
            ProjectId = projectId
        };
    }

    internal static Result<CreateProjectDeliverableDto> ToRequest(ProjectDeliverableViewModel viewModel)
    {
        if (string.IsNullOrWhiteSpace(viewModel.File))
        {
            return Result.Failure<CreateProjectDeliverableDto>(Error.Validation("No se encontraron archivos adjuntos."));
        }

        if (string.IsNullOrWhiteSpace(viewModel.Type))
        {
            return Result.Failure<CreateProjectDeliverableDto>(Error.Validation("No se encontró el tipo de entregable."));
        }

        if (string.IsNullOrWhiteSpace(viewModel.Name))
        {
            return Result.Failure<CreateProjectDeliverableDto>(Error.Validation("No se encontró el nombre del entregable."));
        }

        var response = new CreateProjectDeliverableDto(
            viewModel.Type,
            viewModel.Status.Name,
            viewModel.Name,
            viewModel.File,
            "");

        return Result.Success(response);
    }
}
