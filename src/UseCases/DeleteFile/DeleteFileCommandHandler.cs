using Infrastructure.Abstractions;
using MediatR;
using Saritasa.Tools.EFCore;

namespace UseCases.DeleteFile;

/// Handler for <inheritdoc cref="DeleteFileCommand"/>
internal class DeleteFileCommandHandler : AsyncRequestHandler<DeleteFileCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IFileStorage fileStorage;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="apppDbContext">Application DB context.</param>
    /// <param name="fileStorage">File storage.</param>
    public DeleteFileCommandHandler(IAppDbContext apppDbContext, IFileStorage fileStorage)
    {
        this.appDbContext = apppDbContext;
        this.fileStorage = fileStorage;
    }

    /// <inheritdoc/>
    protected override async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var item = await appDbContext.StoredFiles.GetAsync(file => file.Id == request.Id, cancellationToken);
        await fileStorage.DeleteAsync(request.Id, cancellationToken);
        item.DeletedAt = DateTime.UtcNow;
        await appDbContext.SaveChangesAsync(CancellationToken.None);
    }
}
