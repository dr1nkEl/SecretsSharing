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
}
