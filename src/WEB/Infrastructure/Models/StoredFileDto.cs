using Domain;

namespace WEB.Infrastructure.Models;

/// <summary>
/// Stored file DTO.
/// </summary>
public record StoredFileDto
{
    /// <inheritdoc cref="StoredFile.Id"/>
    public int Id { get; init; }

    /// <inheritdoc cref="StoredFile.Name"/>
    public string Name { get; init; }

    /// <inheritdoc cref="StoredFile.IsDeleting"/>
    public bool IsDeleting { get; init; }

    /// <summary>
    /// Download link.
    /// </summary>
    public string DownloadLink { get; internal set; }
}
