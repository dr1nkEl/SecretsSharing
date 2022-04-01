using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure;

/// <summary>
/// Database file storage.
/// </summary>
public class DBFileStorage : IFileStorage
{
    private readonly IAppDbContext appDbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    /// <param name="loggedUserAccessor">Logged user accessor.</param>
    public DBFileStorage(IAppDbContext appDbContext, ILoggedUserAccessor loggedUserAccessor)
    {
        this.appDbContext = appDbContext;
        this.loggedUserAccessor = loggedUserAccessor;
    }
    
    /// <inheritdoc/>
    public Task DeleteFileAsync(string fileId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task DownloadAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task UplaodAsync(IFormFile file, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task UploadTextAsync(string text, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
