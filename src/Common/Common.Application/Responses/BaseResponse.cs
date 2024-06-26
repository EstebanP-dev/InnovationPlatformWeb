﻿namespace Common.Application.Responses;

public sealed class BaseResponse
{
    [JsonPropertyName("errors")]
    public IEnumerable<BaseErrorResponse> Errors { get; set; } = [];

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
