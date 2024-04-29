namespace Common.Infrastructure.DelegatingHandlers;

internal sealed class RetryDelegatingHandler(ILocalizationResourceManager resourceManager)
    : DelegatingHandler, IScopedDependency
{
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy =
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .RetryAsync(2);

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var policyResult = await _retryPolicy.ExecuteAndCaptureAsync(
            () => base.SendAsync(request, cancellationToken))
            .ConfigureAwait(false);

        if (policyResult.Outcome == OutcomeType.Failure)
        {
            throw policyResult.FinalException
                  ?? new HttpRequestException(
                      resourceManager[GeneralMessagesKeys.Exception.Unexpected] ?? $"{nameof(RetryDelegatingHandler)}: Unexpected error.");
        }

        return policyResult.Result;
    }
}
