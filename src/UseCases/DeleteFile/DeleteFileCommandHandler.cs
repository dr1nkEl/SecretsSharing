using Infrastructure.Abstractions;
using MediatR;

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
    protected override Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
