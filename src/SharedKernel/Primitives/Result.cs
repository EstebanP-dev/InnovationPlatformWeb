namespace SharedKernel.Primitives;

public class Result
{
#pragma warning disable CA1819
    public Error[] Errors { get; }
#pragma warning restore CA1819

    public bool IsSuccess { get; private set; }

    public bool IsFailure => !IsSuccess;

    public Error FirstError => Errors.FirstOrDefault() ?? Error.None();

    protected Result(bool isSuccess, Error? error)
    {
        if (error is null || isSuccess && error.Code != Error.None().Code || !isSuccess && error.Code == Error.None().Code)
            throw new InvalidErrorValueObjectHandlerException();

        IsSuccess = isSuccess;
        Errors = [error];
    }

    protected Result(bool isSuccess, Error[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success() => new(true, Error.None());

    public static Result<TValue> Success<TValue>(TValue? value)
    {
        return value is null
            ? Failure<TValue>(Error.Unexpected(message: "Value cannot be null."))
            : new Result<TValue>(value, true, Error.None());
    }

    public static Result Failure(Error? error) => new(false, error);
    public static Result Failure(Error[] errors) => new(false, errors);
    public static Result<TValue> Failure<TValue>(Error? error) => new(default!, false, error);
    public static Result<TValue> Failure<TValue>(Error[] errors) => new(default!, false, errors);

    public static Result<TValue> Create<TValue>(TValue? value, Error nullValueError)
            => value is null ? Failure<TValue>(error: nullValueError) : Success(value);

    public static async Task<Result<TValue>> Create<TValue>(Task<TValue?>? valueTask, Error? nullValueError)
    {
        ArgumentNullException.ThrowIfNull(valueTask);
        ArgumentNullException.ThrowIfNull(nullValueError);

        TValue? value = await valueTask.ConfigureAwait(false);

        if (value is not null)
            Success(value);

        return Failure<TValue>(error: nullValueError);
    }

    public static Result<TValue> Create<TValue>(Either<TValue, Error>? either, Error? nullValueError)
    {
        ArgumentNullException.ThrowIfNull(either);
        ArgumentNullException.ThrowIfNull(nullValueError);

            return either.Match(
                Success,
                Failure<TValue>);
    }

    public static async Task<Result<TValue>> Create<TValue>(Task<Either<TValue, Error>>? eitherTask, Error? nullValueError)
    {
        ArgumentNullException.ThrowIfNull(eitherTask);

        Either<TValue, Error> either = await eitherTask.ConfigureAwait(false);

        return Create(either: either, nullValueError: nullValueError);
    }

    public static Result FirstFailureOrSuccess(params Result[]? results)
    {
        ArgumentNullException.ThrowIfNull(results);

        foreach (Result result in results)
        {
            if (result.IsFailure)
            {
                return result;
            }
        }

        return Success();
    }

    public static Result<TValue> Combine<TValue>(params Result<TValue>[]? results)
    {
        ArgumentNullException.ThrowIfNull(results);

        if (results.Any(result => result.IsFailure))
            return Failure<TValue>(
                results
                    .SelectMany(result => result.Errors)
                    .Where(error => error.Code != Error.None().Code)
                    .Distinct()
                    .ToArray());

        return Success(results[0].Value);
    }

    public static Result<(T1, T2)> Combine<T1, T2>(Result<T1>? result, Result<T2>? result2)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(result2);

        if (result.IsFailure)
            return Failure<(T1, T2)>(result.Errors);

        return result2.IsFailure ? Failure<(T1, T2)>(result2.Errors) : Success((result.Value, result2.Value));
    }


}

public class Result<TValue> : Result
{
    private readonly TValue _value;

    public TValue Value => IsSuccess
            ? _value
#pragma warning disable CA1065
            : throw new InvalidValuePropertyWhenResultIsFailureException();
#pragma warning restore CA1065

    public Result(TValue value, bool isSuccess, Error? error) : base(isSuccess, error)
        => _value = value;
    protected internal Result(TValue value, bool isSuccess, Error[] errors) : base(isSuccess, errors)
        => _value = value;

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public Result<TValue> ToResult(TValue? value)
    {
        return Success(value);
    }
}
