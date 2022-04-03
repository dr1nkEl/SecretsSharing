using System.ComponentModel.DataAnnotations;

namespace Domain;

/// <summary>
/// Stored file.
/// </summary>
public record StoredFile
{
    /// <summary>
    /// ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of file.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Associated user ID.
    /// </summary>
    [Required]
    public int? AssociatedUserId { get; set; }

    /// <summary>
    /// Associated user.
    /// </summary>
    public User AssociatedUser { get; set; }

    /// <summary>
    /// Deleted at.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}
