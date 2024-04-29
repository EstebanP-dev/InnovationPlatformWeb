namespace Common.Application.Events;

public abstract record ApplicationEvent(Guid Id) : INotification;
