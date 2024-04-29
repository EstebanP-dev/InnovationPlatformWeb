namespace Common.Application.Messaging;

#pragma warning disable CA1040
public interface ICommand<TResponse>
    : IRequest<Result<TResponse>>;

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;

public interface ISuccessCommand
    : ICommand<Success>;

public interface ISuccessCommandHandler<in TSuccessCommand>
    : IRequestHandler<TSuccessCommand, Result<Success>>
    where TSuccessCommand : ISuccessCommand;

public interface IDeletedCommand
    : ICommand<Deleted>;

public interface IDeletedCommandHandler<in TDeletedCommand>
    : IRequestHandler<TDeletedCommand, Result<Deleted>>
    where TDeletedCommand : IDeletedCommand;
#pragma warning restore CA1040
