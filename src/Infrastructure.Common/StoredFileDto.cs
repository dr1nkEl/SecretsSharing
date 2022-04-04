using Domain;


namespace Infrastructure.Common;

/// <summary>
/// Stored file DTO.
/// </summary>
public record StoredFileDto
{
    /// <inheritdoc cref="StoredFile.Name"/>
    public string Name { get; set; }

    /// <inheritdoc cref="StoredFile.AssociatedUserId"/>
    public int AssociatedUserId { get; set; }
}
