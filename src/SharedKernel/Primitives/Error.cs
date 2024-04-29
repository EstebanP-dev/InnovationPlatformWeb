namespace SharedKernel.Primitives;

public sealed class Error
    : ValueObject
{
    public string Code { get; }
    public string Message { get; }
    public BaseException? Exception { get; }

    private Error(string code, string message, BaseException? exception = null)
    {
        Code = code;
        Message = message;
        Exception = exception;
    }

    public static implicit operator string(Error error) => error?.Message ?? string.Empty;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Message;
    }

    public override string ToString()
    {
        var format = CompositeFormat.Parse("Message: {0}, Code: {1}");

        return string.Format(null, format, Message, Code);
    }

    public static Error None()
        => new("200", "No errors.");

    public static Error Failure(string code = "400", string message = "An error occurred.")
        => new(code, message);

    public static Error NotFound(string code = "404", string message = "Resource not found.")
        => new(code, message);

    public static Error Unauthorized(string code = "401", string message = "Unauthorized.")
        => new(code, message);

    public static Error Unexpected(string code = "500", string message = "An unexpected error occurred.")
        => new(code, message);

    public static Error Validation(string code = "400", string message = "Validation error.")
        => new(code, message);

    public static Error Custom(string code, string message)
        => new(code, message);
}
