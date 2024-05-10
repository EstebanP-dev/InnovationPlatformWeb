using System.ComponentModel.DataAnnotations;
using Modules.Projects.Application.Enumerations;
using Modules.Projects.Application.Projects.CreateProject;
using Modules.Projects.Application.Projects.GetProject;
using Modules.Projects.Presentation.Deliverables;
using Modules.Projects.Presentation.ProjectMembers;

namespace Modules.Projects.Presentation.Projects.Form;

public sealed class FormViewModel : BaseViewModel
{
    [Required]
    [StringLength(255, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;

    [StringLength(255, MinimumLength = 2)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Type { get; set; } = string.Empty;

#pragma warning disable CA2227
    public ProjectMembersCollection Assessors { get; set; } = [];

    public ProjectMembersCollection Authors { get; set; } = [];

    public ProjectDeliverablesCollection Deliverables { get; set; } = [];
#pragma warning restore CA2227

    public override bool IsValid()
    {
        ValidateAllProperties();

        var isValid = base.IsValid();

        var deliverablesResult = Deliverables.Select(x =>
        {
            var result = x.IsValid();

            ErrorsCollection.AddRange(x.ErrorsCollection);

            return result;
        });

        return isValid && deliverablesResult.All(x => x);
    }

    internal static FormViewModel FromGetProjectResponse(GetProjectResponse? response)
    {
        ArgumentNullException.ThrowIfNull(response);

        var viewModel = new FormViewModel
        {
            Title = response.Title ?? "",
            Description = response.Description ?? "",
            Type = response.Type ?? "",
            Assessors =
            [
                ProjectMemberViewModel.FromGetProjectMemberResponse(response.Assessor)
            ],
            Authors =
            [
                ..response
                    .Authors
                    .Select(ProjectMemberViewModel.FromGetProjectMemberResponse)
            ],
            Deliverables =
            [
                ..response
                    .Deliverables
                    .Select(ProjectDeliverableViewModel.FromGetProjectDeliverableResponse)
            ]
        };

        return viewModel;
    }

    internal static CreateProjectCommand? ToCreateRequest(FormViewModel? viewModel, out string message)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        message = string.Empty;

        if (!viewModel.Assessors.Any())
        {
            message = "Al menos un asesor debe ser seleccionado.";
            return null;
        }

        if (!viewModel.Authors.Any())
        {
            message = "Al menos un autor debe ser seleccionado.";
            return null;
        }

        if (!viewModel.Deliverables.Any())
        {
            message = "Al menos un entregable debe ser añadido.";
            return null;
        }

        var assesor = viewModel.Assessors.First();

        var deliverablesResult = viewModel
            .Deliverables
            .Select(ProjectDeliverableViewModel.ToRequest)
            .ToArray();

        if (deliverablesResult.Any(x => x.Item1 is null))
        {
            message = "Uno de los archivos es inválido.";
            return null;
        }

        var deliverables = deliverablesResult
            .Select(x => x.Item1!);

        var request = new CreateProjectCommand(
            assesor.Id,
            viewModel.Type,
            viewModel.Title,
            viewModel.Description,
            viewModel.Authors.Select(author => author.Id),
            deliverables,
            ProjectStatusEnumeration.Pending.Name);

        return request;
    }
}
