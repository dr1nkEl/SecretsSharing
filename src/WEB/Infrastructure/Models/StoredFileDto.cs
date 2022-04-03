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
}
