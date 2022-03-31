namespace Domain;

/// <summary>
/// User credentials.
/// </summary>
public record UserCredentials
{
    /// <summary>
    /// Username.
    /// </summary>
    public string UserName { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; init; }
}
