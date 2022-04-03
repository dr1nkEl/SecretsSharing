using AutoMapper;
using Domain;
using Infrastructure.Abstractions;
using MediatR;

namespace UseCases;

/// Handler for <inheritdoc cref="UploadFileCommand"/>
internal class UploadFileCommandHandler : AsyncRequestHandler<UploadFileCommand>
{
    private readonly IFileStorage fileStorage;
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="fileStorage">File storage.</param>
    /// <param name="appDbContext">Application DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public UploadFileCommandHandler(IFileStorage fileStorage, IAppDbContext appDbContext, IMapper mapper)
    {
        this.fileStorage = fileStorage;
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    protected override async Task Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var response = await fileStorage.UploadAsync(request.File, cancellationToken);
        var item = mapper.Map<StoredFile>(response.Result);
        await appDbContext.StoredFiles.AddAsync(item, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
