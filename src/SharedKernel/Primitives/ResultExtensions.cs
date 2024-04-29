namespace SharedKernel.Primitives;

public static class ResultExtensions
{
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn>? result, Func<TIn, TOut>? mappingFunction)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(mappingFunction);

        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : Result.Success(mappingFunction(result.Value));
    }

    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>>? resultTask, Func<TIn, TOut>? mappingFunction)
    {
        ArgumentNullException.ThrowIfNull(resultTask);
        ArgumentNullException.ThrowIfNull(mappingFunction);

        Result<TIn> result = await resultTask.ConfigureAwait(false);

        return result.IsSuccess
            ? Result.Success(mappingFunction(result.Value))
            : Result.Failure<TOut>(result.Errors);
    }

    public static Result<TValue> Ensure<TValue>(this Result<TValue>? result, Func<TValue, bool>? predicate, Error? error)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(predicate);

        if (result.IsFailure)
            return Result.Failure<TValue>(result.Errors);

        return predicate(result.Value)
            ? Result.Success(result.Value)
            : Result.Failure<TValue>(error);
    }

    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>>? resultTask, Func<TValue, bool>? predicate, Error? error)
    {
        ArgumentNullException.ThrowIfNull(resultTask);
        ArgumentNullException.ThrowIfNull(predicate);

        var result = await resultTask
            .ConfigureAwait(false);

        if (result.IsFailure)
            return Result.Failure<TValue>(result.Errors);

        return predicate(result.Value)
            ? Result.Success(result.Value)
            : Result.Failure<TValue>(error);
    }

    public static Result<TValue> Ensure<TValue>(this Result<TValue> result, params (Func<TValue, bool> predicate, Error error)[]? functions)
    {
        ArgumentNullException.ThrowIfNull(functions);

        List<Result<TValue>> results = [];

        foreach ((Func<TValue, bool> predicate, Error error) in functions)
        {
            results.Add(result.Ensure(predicate, error));
        }

        return Result.Combine(results.ToArray());
    }

    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, params (Func<TValue, bool> predicate, Error error)[]? functions)
    {
        ArgumentNullException.ThrowIfNull(resultTask);
        ArgumentNullException.ThrowIfNull(functions);

        var result = await resultTask
            .ConfigureAwait(false);

        List<Result<TValue>> results = [];

        foreach ((Func<TValue, bool> predicate, Error error) in functions)
        {
            results.Add(result.Ensure(predicate, error));
        }

        return Result.Combine(results.ToArray());
    }

    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn>? result, Func<TIn, Result<TOut>>? bindFunction)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(bindFunction);

        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : bindFunction(result.Value);
    }

    public static Result Bind<TIn>(this Result<TIn>? result, Func<TIn, Result>? bindFunction)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(bindFunction);

        return result.IsFailure
            ? Result.Failure(result.Errors)
            : bindFunction(result.Value);
    }


    public static async Task<Result<TOut>> Bind<TIn, TOut>(
        this Result<TIn>? result,
        Func<TIn, Task<Result<TOut>>>? bindFunction)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(bindFunction);

        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : await bindFunction(result.Value)
                .ConfigureAwait(false);
    }

    public static async Task<Result> Bind<TIn>(
        this Result<TIn>? result,
        Func<TIn, Task<Result>>? bindFunction)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(bindFunction);

        return result.IsFailure
            ? Result.Failure(result.Errors)
            : await bindFunction(result.Value)
                .ConfigureAwait(false);
    }
    public static async Task<Result> Bind<TIn>(
        this Task<Result<TIn>>? resultTask,
        Func<TIn, Task<Result>>? bindFunction)
    {
        ArgumentNullException.ThrowIfNull(resultTask);
        ArgumentNullException.ThrowIfNull(bindFunction);

        Result<TIn> result = await resultTask
            .ConfigureAwait(false);

        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return await bindFunction(result.Value)
            .ConfigureAwait(false);
    }

    public static async Task<Result<TOut>> Bind<TIn, TOut>(
        this Task<Result<TIn>>? resultTask,
        Func<TIn, Result<TOut>>? bindFunction)
    {
        ArgumentNullException.ThrowIfNull(resultTask);
        ArgumentNullException.ThrowIfNull(bindFunction);

        Result<TIn> result = await resultTask
            .ConfigureAwait(false);

        return result.IsFailure
            ? Result.Failure<TOut>(result.Errors)
            : bindFunction(result.Value);
    }

    public static Result<TIn> Tap<TIn>(this Result<TIn>? result, Action<TIn>? action)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.IsSuccess)
        {
            action?.Invoke(result.Value);
        }

        return result;
    }

    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>>? resultTask, Func<TIn, Task>? func)
    {
        ArgumentNullException.ThrowIfNull(resultTask);
        ArgumentNullException.ThrowIfNull(func);

        Result<TIn> result = await resultTask
            .ConfigureAwait(false);

        if (result.IsSuccess)
        {
            await func(result.Value)
                .ConfigureAwait(false);
        }

        return result;
    }

    public static async Task<TOut> Match<TOut>(
        this Task<Result>? resultTask,
        Func<TOut>? onSuccess,
        Func<Result, TOut>? onFailure)
    {
        ArgumentNullException.ThrowIfNull(resultTask);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        Result result = await resultTask
            .ConfigureAwait(false);

        return result.IsFailure
            ? onFailure(result)
            : onSuccess();
    }

    public static async Task<TOut> Match<TIn, TOut>(
        this Task<Result<TIn>>? resultTask,
        Func<TIn, TOut>? onSuccess,
        Func<Result<TIn>, TOut>? onFailure)
    {
        ArgumentNullException.ThrowIfNull(resultTask);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        Result<TIn> result = await resultTask
            .ConfigureAwait(false);

        return result.IsFailure
            ? onFailure(result)
            : onSuccess(result.Value);
    }

    public static async Task Match(this Task<Result>? resultTask, Func<Task>? onSuccess, Func<Result, Task>? onFailure)
    {
        ArgumentNullException.ThrowIfNull(resultTask);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        Result result = await resultTask
            .ConfigureAwait(false);

        if (result.IsFailure)
        {
            await onFailure(result)
                .ConfigureAwait(false);
            return;
        }

        await onSuccess()
            .ConfigureAwait(false);
    }
}
