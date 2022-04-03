namespace Infrastructure.Common;

/// <summary>
/// Download file DTO.
/// </summary>
public record DownloadFileDto
{
    /// <summary>
    /// Response stream.
    /// </summary>
    public Stream ResponseStream { get; init; }

    /// <summary>
    /// File name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Content type.
    /// </summary>
    public string ContentType { get; init; }
}
