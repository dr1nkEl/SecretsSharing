using Domain;

namespace Infrastructure.Abstractions;

/// <summary>
///  Abstraction to access logged user.
/// </summary>
public interface ILoggedUserAccessor
{
    /// <summary>
    /// Get logged user.
    /// </summary>
    /// <returns>User.</returns>
    Task<User> GetMeAsync(CancellationToken cancellationToken = default);
}
