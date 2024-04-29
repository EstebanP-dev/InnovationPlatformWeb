using System.Runtime.Serialization;

namespace SharedKernel.Exceptions;

public abstract class BaseException : SystemException
{
    protected BaseException(string? message) : base(message)
    {
    }

    protected BaseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BaseException()
    {
    }
}
