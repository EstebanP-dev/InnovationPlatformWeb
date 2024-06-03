using System.Security.Claims;
using Common.Application.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using Modules.Projects.Application.Assessors.GetAssessors;
using Modules.Projects.Application.Authors.GetAuthors;
using Modules.Projects.Application.Enumerations;
using Modules.Projects.Presentation.ProjectMembers;

namespace Modules.Projects.Presentation.Projects.Form;

public sealed partial class MembersInformation
{
    // private readonly Dispatcher _dispatcher = Dispatcher.CreateDefault();
    private readonly ProjectMembersCollection _data = [];
    private ClaimsPrincipal? _userClaimsPrincipal;
    private string? _userId;

    [Inject]
    private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

    [Inject]
    private IAuthSessionStorage? AuthSessionStorage { get; set; }

    [Parameter]
    public string? AddButtonTitle { get; set; }

    [Parameter]
    public int MaximumMembers { get; set; } = 3;

    [Parameter]
    public bool LoadAssessors { get; set; }

    [Parameter]
#pragma warning disable CA2227
    public ProjectMembersCollection SelectedItems { get; set; } = [];
#pragma warning restore CA2227

    [Parameter]
    public EventCallback<ProjectMembersCollection> SelectedItemsChanged { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public bool FromCreate { get; set; } = true;

    [Parameter]
    public ProjectStatusEnumeration Status { get; set; } = ProjectStatusEnumeration.Pending;

    [Inject]
    private ISender? Sender { get; init; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadMembers();
    }

    private void LoadMembers()
    {
        ArgumentNullException.ThrowIfNull(Sender);

        if (LoadAssessors)
        {
            InvokeAsync(async () =>
            {
                var result = await Sender
                    .Send(new GetAssessorsQuery())
                    .ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    _data.AddRangeAndClear(ProjectMemberViewModel.FromAssessorsResponse(result.Value));
                }

                await GetUserInformation().ConfigureAwait(false);

                AddCurrentUser();
            });
        }
        else
        {
            InvokeAsync(async () =>
            {
                var result = await Sender
                    .Send(new GetAuthorsQuery())
                    .ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    var authors = ProjectMemberViewModel.FromAuthorsResponse(result.Value);

                    _data.AddRangeAndClear(authors);
                }

                await GetUserInformation().ConfigureAwait(false);

                AddCurrentUser();
            });
        }
    }

    private void AddCurrentUser()
    {
        if (!FromCreate)
        {
            return;
        }

        var role = LoadAssessors ? "Assessor" : "Student";

        if (!UserMatchRole(role))
        {
            return;
        }

        if (_data.Count == 0)
        {
            return;
        }

        var user = _data.FirstOrDefault(x => x.Id == _userId);

        if (user is null)
        {
            return;
        }

        user.CanRemoveItem = false;

        SelectedItems.AddAndClear(user);
        StateHasChanged();
    }

    private async Task GetUserInformation()
    {
        ArgumentNullException.ThrowIfNull(AuthenticationStateProvider);
        ArgumentNullException.ThrowIfNull(AuthSessionStorage);

        var authState = await AuthenticationStateProvider
            .GetAuthenticationStateAsync()
            .ConfigureAwait(false);

        ArgumentNullException.ThrowIfNull(authState);

        var userId = await AuthSessionStorage
            .Get<string>(AuthSessionKeys.UserId)
            .ConfigureAwait(false);

        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        _userClaimsPrincipal = authState.User;
        _userId = userId;
    }

    private bool UserMatchRole(string role)
    {
        ArgumentNullException.ThrowIfNull(_userClaimsPrincipal);

        return _userClaimsPrincipal.IsInRole("Admin") || _userClaimsPrincipal.IsInRole(role);
    }
}
