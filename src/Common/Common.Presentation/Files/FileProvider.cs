using Common.Application.Files;
using Microsoft.AspNetCore.Components.Forms;

namespace Common.Presentation.Files;

public sealed class FileProvider(IBrowserFile browserFile)
    : IFileProvider
{
    public string Name => browserFile.Name;
    public DateTimeOffset LastModified => browserFile.LastModified;
    public long Size => browserFile.Size;
    public string ContentType => browserFile.ContentType;

    public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
    {
        return browserFile.OpenReadStream(maxAllowedSize, cancellationToken);
    }
}
