using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SharedKernel.Primitives;
using ValidationException = FluentValidation.ValidationException;

namespace Common.Infrastructure.PipelineBehaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<IRequest>> enumerableValidators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        var validationFailures = await ValidateAsync(request, cancellationToken)
            .ConfigureAwait(false);

        if (validationFailures.Length == 0)
        {
            return await next()
                .ConfigureAwait(false);
        }

        if (typeof(TResponse).IsGenericParameter &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0];

            var failureMethod = typeof(Result)
                .MakeGenericType(resultType)
                .GetMethod(nameof(Result<object>.Failure));

            if (failureMethod is not null)
            {
                return ((TResponse)failureMethod.Invoke(
                    null,
                    new object[] { CreateValidatorErrors(validationFailures) })!);
            }
        }
        else if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(CreateValidatorErrors(validationFailures));
        }

        throw new ValidationException(validationFailures);
    }

    private async Task<ValidationFailure[]> ValidateAsync(
        TRequest request,
        CancellationToken cancellationToken)
    {
        if (!enumerableValidators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task
            .WhenAll(enumerableValidators
                .Select(v => v
                    .ValidateAsync(context, cancellationToken)))
            .ConfigureAwait(false);

        var validationFailures = validationResults
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)
            .ToArray();

        return validationFailures;
    }

    private static Error[] CreateValidatorErrors(IEnumerable<ValidationFailure> validationFailures)
    {
        return validationFailures
            .Select(failure => Error.Validation(message: failure.ErrorMessage))
            .ToArray();
    }
}
