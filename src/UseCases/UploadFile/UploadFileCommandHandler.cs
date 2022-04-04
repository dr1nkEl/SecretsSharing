using AutoMapper;
using Domain;
using Infrastructure.Abstractions;
using MediatR;
using UseCases.Common;

namespace UseCases;

/// Handler for <inheritdoc cref="UploadFileCommand"/>
internal class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Response<int>>
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
    public async Task<Response<int>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var response = await fileStorage.UploadAsync(request.File, cancellationToken);
        var item = mapper.Map<StoredFile>(response.Result);
        item.IsDeleting = request.IsDeleting;
        await appDbContext.StoredFiles.AddAsync(item, CancellationToken.None);
        await appDbContext.SaveChangesAsync(CancellationToken.None);
        return new Response<int>
        {
            Result = item.Id,
        };
    }
}
