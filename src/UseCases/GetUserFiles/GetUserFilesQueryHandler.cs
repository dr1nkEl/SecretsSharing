using Infrastructure.Abstractions;
using MediatR;
using UseCases.Common;

namespace UseCases;

/// Handler for <inheritdoc cref="GetUserFilesQuery"/>
internal class GetUserFilesQueryHandler : IRequestHandler<GetUserFilesQuery, IEnumerable<StoredFileDto>>
{
    private readonly IAppDbContext appDbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Application DB context.</param>
    public GetUserFilesQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<StoredFileDto>> Handle(GetUserFilesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
