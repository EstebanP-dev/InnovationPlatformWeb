namespace Common.Application.Messaging;

#pragma warning disable CA1040
public interface IQuery<TResponse>
    : IRequest<Result<TResponse>>;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
#pragma warning restore CA1040
