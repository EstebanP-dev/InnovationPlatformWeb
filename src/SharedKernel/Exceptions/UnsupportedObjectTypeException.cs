using System.Diagnostics.CodeAnalysis;

namespace SharedKernel.Exceptions;

#pragma warning disable CA1032
public class UnsupportedObjectTypeException(string type, Exception? innerException = default)
#pragma warning restore CA1032
    : SystemException($"It only support '{type}' type.", innerException)
{
    [DoesNotReturn]
    public static void Throw(string type, Exception? innerException = default)
        => throw new UnsupportedObjectTypeException(type, innerException);
}
