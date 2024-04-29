using MediatR;
using SharedKernel.Primitives;

namespace Common.Infrastructure.PipelineBehaviors;

public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : notnull
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly Func<ILogger, string, string, string, IDisposable?> LoggerMessage =
        Microsoft.Extensions.Logging.LoggerMessage
            .DefineScope<string, string, string>("{Action} {RequestName} {Complement}".Trim());

    private static void LogRequestScope(ILogger logger, string requestName)
    {
        LoggerMessage(logger, "Processing request", requestName, "");
    }

    private static void LogCompleteRequestScope(ILogger logger, string requestName)
    {
        LoggerMessage(logger, "Complete request", requestName, "");
    }

    private static void LogCompleteRequestWithErrorScope(ILogger logger, string requestName, Error error)
    {
        LoggerMessage(logger, "Complete request", requestName, $"with error: {error.Code}-{error.Message}");
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        var requestName = typeof(TRequest).Name;

        LogRequestScope(logger, requestName);

        TResponse response = await next()
            .ConfigureAwait(false);

        if (response is Result result)
        {
            if (result.IsSuccess)
            {
                LogCompleteRequestScope(logger, requestName);
            }
            else
            {
                LogCompleteRequestWithErrorScope(logger, requestName, result.FirstError);
            }
        }

        return response;
    }
}
