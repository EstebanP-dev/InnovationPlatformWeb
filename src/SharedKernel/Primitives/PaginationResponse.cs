namespace SharedKernel.Primitives;

public sealed record PaginationResponse<T>(
        int PageNumber,
        int PageSize,
        int TotalPages,
        int TotalCount,
        IEnumerable<T> Items)
    where T : class;

public sealed record PaginationCursorResponse<T>(
        int NextCursor,
        int PreviousCursor,
        IEnumerable<T> Items)
    where T : class;
