using System.ComponentModel.DataAnnotations;
using Modules.Projects.Application.Enumerations;
using Modules.Projects.Application.Projects.CreateProject;
using Modules.Projects.Application.Projects.GetProject;
using Modules.Projects.Application.Projects.UpdateProject;
using Modules.Projects.Presentation.Deliverables;
using Modules.Projects.Presentation.ProjectMembers;
using SharedKernel.Primitives;

namespace Modules.Projects.Presentation.Projects.Form;

public sealed class FormViewModel : BaseViewModel
{
    public string? Id { get; set; } = string.Empty;

    [Required]
    [StringLength(255, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;

    [StringLength(255, MinimumLength = 2)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Type { get; set; } = string.Empty;

    [Required]
    public ProjectStatusEnumeration Status { get; set; } = ProjectStatusEnumeration.Pending;

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

    internal static Result<FormViewModel> FromGetProjectResponse(GetProjectResponse? response)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(response.Id);

        var status = ProjectStatusEnumeration.FromName(response.Status);
        status ??= ProjectStatusEnumeration.Pending;

        var viewModel = new FormViewModel
        {
            Id = response.Id,
            Status = status,
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
                    .Select(x => ProjectDeliverableViewModel.FromGetProjectDeliverableResponse(x, response.Id))
            ]
        };

        return viewModel;
    }

    internal static Result<CreateProjectCommand> ToCreateRequest(FormViewModel? viewModel, string deliverableFolder)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        if (!viewModel.Assessors.Any())
        {
            return Result.Failure<CreateProjectCommand>(Error.Validation(message: "Al menos un asesor debe ser seleccionado."));
        }

        if (!viewModel.Authors.Any())
        {
            return Result.Failure<CreateProjectCommand>(Error.Validation(message:"Al menos un autor debe ser seleccionado."));
        }

        if (!viewModel.Deliverables.Any())
        {
            return Result.Failure<CreateProjectCommand>(Error.Validation(message: "Al menos un entregable debe ser seleccionado."));
        }

        var assesor = viewModel.Assessors.First();

        var deliverablesResults = viewModel
            .Deliverables
            .Select(ProjectDeliverableViewModel.ToRequest)
            .ToArray();

        // ReSharper disable once CoVariantArrayConversion
        var deliverablesResult = Result.FirstFailureOrSuccess(deliverablesResults);

        if (deliverablesResult.IsFailure)
        {
            return Result.Failure<CreateProjectCommand>(deliverablesResult.Errors);
        }

        var deliverables = deliverablesResults
            .Select(x => x.Value);

        var request = new CreateProjectCommand(
            assesor.Id,
            viewModel.Type,
            viewModel.Title,
            viewModel.Description,
            deliverableFolder,
            viewModel.Authors.Select(author => author.Id),
            deliverables,
            ProjectStatusEnumeration.Pending.Name);

        return request;
    }

    internal static Result<UpdateProjectCommand> ToUpdateRequest(FormViewModel? viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        if (string.IsNullOrWhiteSpace(viewModel.Id))
        {
            return Result.Failure<UpdateProjectCommand>(Error.Validation(message: "No se encuentra el projecto seleccionado."));
        }

        if (string.IsNullOrWhiteSpace(viewModel.Title))
        {
            return Result.Failure<UpdateProjectCommand>(Error.Validation(message: "El título es requerido."));
        }

        if (string.IsNullOrWhiteSpace(viewModel.Description))
        {
            return Result.Failure<UpdateProjectCommand>(Error.Validation(message: "La descripción es requerida."));
        }

        var command = new UpdateProjectCommand(
            viewModel.Id,
            Title: viewModel.Title,
            Description: viewModel.Description);

        return command;
    }
}
