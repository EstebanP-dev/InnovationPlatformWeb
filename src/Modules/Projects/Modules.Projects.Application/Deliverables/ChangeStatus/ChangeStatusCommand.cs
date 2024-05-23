namespace Modules.Projects.Application.Deliverables.ChangeStatus;

public sealed record ChangeStatusCommand(
        string ProjectId,
        string DeliverableId,
        string Status)
    : ICommand<Updated>;
