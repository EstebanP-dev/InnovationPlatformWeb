using CommunityToolkit.Mvvm.Messaging.Messages;
using Modules.Projects.Application.Enumerations;

namespace Modules.Projects.Presentation.Messages;

public sealed class StatusChanged(ProjectStatusEnumeration value)
    : ValueChangedMessage<ProjectStatusEnumeration>(value);
