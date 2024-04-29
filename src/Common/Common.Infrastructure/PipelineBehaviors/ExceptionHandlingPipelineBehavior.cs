using System.Net;
using System.Reflection;
using MediatR;
using SharedKernel.Primitives;

namespace Common.Infrastructure.PipelineBehaviors;

internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
        ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly Action<ILogger, string, Exception?> LoggingScope =
        LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(1, "ExceptionHandlingPipelineBehavior"),
            "Unhandled exception for {RequestName}");

    private static void LogException(ILogger logger, string requestName, Exception? exception = null)
    {
        LoggingScope(logger, requestName, exception);
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next()
                .ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {

            var error = ex.StatusCode switch
            {
                HttpStatusCode.BadRequest => Error.Failure(),
                HttpStatusCode.Unauthorized => Error.Unauthorized(),
                HttpStatusCode.Forbidden => Error.Unauthorized(),
                HttpStatusCode.NotFound => Error.NotFound(),
                _ => Error.Unexpected()
            };

            return ToTResponse(error);
        }
#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
            LogException(logger, request.GetType().Name, ex);

            return ToTResponse(Error.Unexpected());
        }
    }

    private TResponse ToTResponse(Error error)
    {
        Error[] errors = [error];

        var methods = typeof(Result)
            .GetMethods(BindingFlags.Static | BindingFlags.Public);

        var methodInfo = methods
            .First(m => m is { Name: nameof(Result<object>.Failure), IsGenericMethod: true, IsGenericMethodDefinition: true } &&
                        m.GetParameters().Length == 1 &&
                        m.GetParameters()[0].ParameterType is { Name: "Error[]" });

        var genericParameter = typeof(TResponse).GetGenericArguments().First();

        ArgumentNullException.ThrowIfNull(methodInfo);
        ArgumentNullException.ThrowIfNull(genericParameter);

        var genericMethod = methodInfo.MakeGenericMethod(genericParameter);

        var response = genericMethod.Invoke(null, [errors]);

        ArgumentNullException.ThrowIfNull(response);

        return (TResponse)response;
    }

}
