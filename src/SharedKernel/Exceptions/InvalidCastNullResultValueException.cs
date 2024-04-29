using System.Runtime.CompilerServices;
using Domain.Shared.Errors;

namespace SharedKernel.Exceptions;

#pragma warning disable CA1032
public sealed class InvalidCastNullResultValueException(
    [CallerMemberName] string member = "",
    [CallerLineNumber] int line = 0,
    [CallerFilePath] string path = "")
#pragma warning restore CA1032
    : BaseException(string.Format(
        null,
        CompositeFormat.Parse(SharedErrorsResource.NullValue),
        member,
        line,
        path));
