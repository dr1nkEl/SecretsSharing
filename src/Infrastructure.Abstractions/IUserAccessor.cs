using Domain;

namespace Infrastructure.Abstractions;

/// <summary>
/// Abstraction to access user.
/// </summary>
public interface IUserAccessor
{
    /// <summary>
    /// Get logged user.
    /// </summary>
    /// <returns>User.</returns>
    Task<User> GetMeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<User> Get(int id, CancellationToken cancellationToken = default);
}
