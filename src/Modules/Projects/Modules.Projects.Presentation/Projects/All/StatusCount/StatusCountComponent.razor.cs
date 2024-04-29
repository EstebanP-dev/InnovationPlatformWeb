using MediatR;
using Microsoft.FluentUI.AspNetCore.Components;
using Modules.Projects.Application.GetTotalStatusCountByUser;

namespace Modules.Projects.Presentation.Projects.All.StatusCount;

public sealed partial class StatusCountComponent
{
    private readonly GetTotalStatusCountByUserCollection _getTotalStatusCountByUserCollection = [];

    [Inject]
    public ISender? Sender { get; init; }

    [Inject]
    public IToastService? ToastService { get; init; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync()
            .ConfigureAwait(false);

        await LoadDataAsync()
            .ConfigureAwait(false);
    }

    private async Task LoadDataAsync()
    {
        ArgumentNullException.ThrowIfNull(Sender);

        var result = await Sender
            .Send(new GetTotalStatusCountByUserQuery())
            .ConfigureAwait(false);

        if (result.IsSuccess)
        {
            var data = StatusCountComponentViewModel
                .FromResponse(result.Value);

            _getTotalStatusCountByUserCollection.AddOnlyMatchingValues(data);
        }
    }
}
