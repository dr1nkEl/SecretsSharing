using System.ComponentModel.DataAnnotations;

namespace WEB.Infrastructure.Models;

/// <summary>
/// User credentials.
/// </summary>
public record UserCredentials
{
    /// <summary>
    /// Email.
    /// </summary>
    [Required]
    public string Email { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    public string Password { get; init; }
}
