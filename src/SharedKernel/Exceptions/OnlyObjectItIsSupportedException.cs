using System.Diagnostics.CodeAnalysis;

namespace SharedKernel.Exceptions;

#pragma warning disable CA1032
public sealed class OnlyObjectItIsSupportedException(string objectName, Exception? innerException = default)
#pragma warning restore CA1032
    : SystemException($"Only object it is supported: {objectName}", innerException)
{
    [DoesNotReturn]
    public static void Throw([NotNull] string objectName, Exception? innerException = default)
        => throw new OnlyObjectItIsSupportedException(objectName, innerException);
}
