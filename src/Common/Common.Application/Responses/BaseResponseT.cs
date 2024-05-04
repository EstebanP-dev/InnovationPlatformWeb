namespace Common.Application.Responses;

public sealed class BaseResponse<TResponse>
{
    [JsonPropertyName("errors")]
    public IEnumerable<BaseErrorResponse> Errors { get; set; } = [];

    [JsonPropertyName("value")]
    public TResponse? Value { get; set; }

    public bool IsSuccess()
    {
        var errors = Errors.ToArray();

        return errors is [{ Code: 200 }];
    }

    public Error[] MapErrors()
    {
        return Errors
            .Select(error => Error
                .Custom($"{error.Code}", error.Message ?? "Unexpected Error"))
            .ToArray();
    }
}
