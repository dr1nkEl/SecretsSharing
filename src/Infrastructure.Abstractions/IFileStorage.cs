using Microsoft.AspNetCore.Http;

namespace Infrastructure.Abstractions;

/// <summary>
/// Abstraction to access file storage.
/// </summary>
public interface IFileStorage
{
    /// <summary>
    /// Downloads file with given ID.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task.</returns>
    Task DownloadAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads file to storage.
    /// </summary>
    /// <param name="file">File.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task.</returns>
    Task UplaodAsync(IFormFile file, CancellationToken cancellationToken = default);

    /// <summary>
    /// Upload text.
    /// Text would be translated to .txt file format.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task.</returns>
    Task UploadTextAsync(string text, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete file.
    /// </summary>
    /// <param name="fileId">File ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task.</returns>
    Task DeleteFileAsync(string fileId, CancellationToken cancellationToken = default);
}
