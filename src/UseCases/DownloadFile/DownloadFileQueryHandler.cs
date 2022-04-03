using Infrastructure.Abstractions;
using MediatR;

namespace UseCases;

/// Handler for <inheritdoc cref="DownloadFileQuery"/>
internal class DownloadFileQueryHandler : AsyncRequestHandler<DownloadFileQuery>
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
    protected override Task Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
