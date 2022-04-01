using Domain;
using System.ComponentModel.DataAnnotations;

namespace UseCases.Common;

/// <summary>
/// Stored file DTO.
/// </summary>
public record StoredFileDto
{
    /// <inheritdoc cref="StoredFile.Id"/>
    public int Id { get; set; }

    /// <inheritdoc cref="StoredFile.Name"/>
    [Required]
    public string Name { get; set; }

    /// <inheritdoc cref="StoredFile.AssociatedUserId"/>
    [Required]
    public int? AssociatedUserId { get; set; }
}
