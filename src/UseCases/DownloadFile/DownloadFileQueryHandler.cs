using Infrastructure.Abstractions;
using Infrastructure.Common;
using MediatR;

namespace UseCases;

/// Handler for <inheritdoc cref="DownloadFileQuery"/>
internal class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileDto>
{
    private readonly IFileStorage fileStorage;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="fileStorage">File storage.</param>
    public DownloadFileQueryHandler(IFileStorage fileStorage)
    {
        this.fileStorage = fileStorage;
    }

    /// <inheritdoc/>
    public async Task<DownloadFileDto> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        return await fileStorage.DownloadAsync(request.Id, cancellationToken);
    }
}
