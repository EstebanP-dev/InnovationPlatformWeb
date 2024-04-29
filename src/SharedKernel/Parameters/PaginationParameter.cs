namespace SharedKernel.Parameters;

public interface IPagination
{
    public int PageSize { get; init; }
}

public sealed record PaginationParameter(
    int PageNumber,
    int PageSize) : IPagination;

public sealed record PaginationByCursorParameter(
    int Cursor,
    int PageSize) : IPagination;
