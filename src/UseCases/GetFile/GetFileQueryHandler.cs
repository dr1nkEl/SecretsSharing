using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using Saritasa.Tools.EFCore;
using UseCases.Common;

namespace UseCases;

/// Handler for <inheritdoc cref="GetFileQuery"/>
internal class GetFileQueryHandler : IRequestHandler<GetFileQuery, StoredFileDto>
{
    private readonly IMapper mapper;
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    /// <param name="mapper">Mapper.</param>
    public GetFileQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<StoredFileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        return await mapper.ProjectTo<StoredFileDto>(appDbContext.StoredFiles)
            .GetAsync(file => file.Id == request.Id, cancellationToken);
    }
}
