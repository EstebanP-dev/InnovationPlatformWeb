﻿using Modules.Projects.Application.Projects.GetProjects;

namespace Modules.Projects.Presentation.Projects.All.List;

public sealed record MemberViewModel(
    string Id,
    string FullName)
{
    internal static MemberViewModel FromResponse(GetProjectsMemberResponse? member)
    {
        return new MemberViewModel(
            member?.Id ?? string.Empty,
            member?.FullName ?? string.Empty);
    }
};
