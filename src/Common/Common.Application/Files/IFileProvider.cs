namespace Common.Application.Files;

public interface IFileProvider
{
    string Name { get; }
    DateTimeOffset LastModified { get; }
    long Size { get; }
    string ContentType { get; }
    Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default);
}
