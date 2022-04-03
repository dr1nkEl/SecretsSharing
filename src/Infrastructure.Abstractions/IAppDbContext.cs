using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions;

/// <summary>
/// Interface of application database context.
/// </summary>
public interface IAppDbContext
{
    /// <summary>
    /// Stored files.
    /// </summary>
    DbSet<StoredFile> StoredFiles { get; }

    /// <summary>
    /// Save changes.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
