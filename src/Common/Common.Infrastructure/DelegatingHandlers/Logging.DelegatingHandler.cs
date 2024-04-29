namespace Common.Infrastructure.DelegatingHandlers;

internal sealed class LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
    : DelegatingHandler, IScopedDependency
{
    private static readonly Action<ILogger, string, string, string, Exception?> LogTraceHandler =
        LoggerMessage.Define<string, string, string>(
            LogLevel.Trace,
            new EventId(0, nameof(LogTraceHandler)),
            "[{TransactionId}] {Type}: {RequestInfo}");

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var transactionId = Guid.NewGuid().ToString();

        var requestContent = string.Empty;

        if (request.Content is not null)
        {
            requestContent = await request
                .Content
                .ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        try
        {
            LogTraceHandler(
                logger,
                transactionId,
                "Request",
                $"{request.Method}|{request.RequestUri}|{requestContent}",
                default);

            var response = await base
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response
                .Content
                .ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);

            LogTraceHandler(
                logger,
                transactionId,
                "Response",
                $"{response.StatusCode}|{responseContent}",
                default);

            return response;
        }
        catch (HttpRequestException ex)
        {
            LogTraceHandler(
                logger,
                transactionId,
                "Response",
                $"{ex.StatusCode}|{ex.Message}",
                ex);

            throw;
        }
        catch (Exception ex)
        {
            LogTraceHandler(
                logger,
                transactionId,
                "Response",
                $"{ex.GetType().Name}|{ex.Message}",
                ex);

            throw;
        }
    }
}
