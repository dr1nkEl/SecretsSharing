using Infrastructure.Abstractions;
using MediatR;

namespace UseCases;

/// Handler for <inheritdoc cref="UploadTextCommand"/>
internal class UploadTextCommandHandler : AsyncRequestHandler<UploadTextCommand>
{
    private readonly IFileStorage fileStorage;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="fileStorage">File storage.</param>
    public UploadTextCommandHandler(IFileStorage fileStorage)
    {
        this.fileStorage = fileStorage;
    }

    protected override async Task Handle(UploadTextCommand request, CancellationToken cancellationToken)
    {
        await fileStorage.UploadTextAsync(request.Text, cancellationToken);
    }
}
