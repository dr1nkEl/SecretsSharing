namespace Domain;

/// <summary>
/// User credentials.
/// </summary>
public record UserCredentials
{
    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; init; }
}
