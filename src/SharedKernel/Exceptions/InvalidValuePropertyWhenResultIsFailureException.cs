namespace SharedKernel.Exceptions;

#pragma warning disable CA1032
public sealed class InvalidValuePropertyWhenResultIsFailureException()
#pragma warning restore CA1032
    : BaseException("The value of a failure result can not be accessed.", null);
