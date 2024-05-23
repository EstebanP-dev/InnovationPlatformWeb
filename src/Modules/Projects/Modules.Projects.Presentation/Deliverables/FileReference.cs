namespace Modules.Projects.Presentation.Deliverables;

public sealed class FileReference(string identifier, string name, byte[] content, string contentType)
{
    public string Identifier { get; set; } = identifier;
    public string Name { get; set; } = name;
#pragma warning disable CA1819
    public byte[] Content { get; set; } = content;
#pragma warning restore CA1819
    public string ContentType { get; set; } = contentType;

    [JSInvokable]
    public byte[] GetFileContent()
    {
        return Content;
    }
}
