using Extensions.Hosting.AsyncInitialization;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace WEB.Infrastructure.Initializers;

/// <summary>
/// Database initializer.
/// </summary>
public class DatabaseInitializer : IAsyncInitializer
{
    private readonly AppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public DatabaseInitializer(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task InitializeAsync()
    {
        await appDbContext.Database.MigrateAsync(CancellationToken.None);
    }
}
