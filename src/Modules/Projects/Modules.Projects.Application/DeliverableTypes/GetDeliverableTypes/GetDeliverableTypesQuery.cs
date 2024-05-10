namespace Modules.Projects.Application.DeliverableTypes.GetDeliverableTypes;

public sealed record GetDeliverableTypesQuery
    : IQuery<IEnumerable<GetDeliverableTypesResponse>>;
