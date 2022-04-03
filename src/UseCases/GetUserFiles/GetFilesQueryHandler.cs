using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Common;

namespace UseCases;

/// Handler for <inheritdoc cref="GetFilesQuery"/>
internal class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, IEnumerable<StoredFileDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public GetFilesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<StoredFileDto>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
        return await mapper
            .ProjectTo<StoredFileDto>(appDbContext.StoredFiles)
            .Where(file=>file.AssociatedUserId == request.UserId)
            .ToListAsync(cancellationToken);
    }
}
