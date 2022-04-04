using AutoMapper;
using Domain;
using Infrastructure.Abstractions;
using MediatR;
using UseCases.Common;

namespace UseCases;

/// Handler for <inheritdoc cref="UploadTextCommand"/>
internal class UploadTextCommandHandler : IRequestHandler<UploadTextCommand, Response<int>>
{
    private readonly IFileStorage fileStorage;
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="fileStorage">File storage.</param>
    public UploadTextCommandHandler(IFileStorage fileStorage, IAppDbContext appDbContext, IMapper mapper)
    {
        this.fileStorage = fileStorage;
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Response<int>> Handle(UploadTextCommand request, CancellationToken cancellationToken)
    {
        var response = await fileStorage.UploadTextAsync(request.Text, cancellationToken);
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
