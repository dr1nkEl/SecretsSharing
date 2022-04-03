using Infrastructure.Abstractions;
using Infrastructure.Common;
using MediatR;
using Saritasa.Tools.EFCore;

namespace UseCases;

/// Handler for <inheritdoc cref="DownloadFileQuery"/>
internal class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileDto>
{
    private readonly IFileStorage fileStorage;
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="fileStorage">File storage.</param>
    /// <param name="appDbContext">Application DB context.</param>
    public DownloadFileQueryHandler(IFileStorage fileStorage, IAppDbContext appDbContext)
    {
        this.fileStorage = fileStorage;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task<DownloadFileDto> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        var file = await appDbContext.StoredFiles.GetAsync(file => file.Id == request.Id, cancellationToken);
        var downloadDto = await fileStorage.DownloadAsync(request.Id, cancellationToken);
        return downloadDto;
    }
}
