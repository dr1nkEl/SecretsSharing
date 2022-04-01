using AutoMapper;
using Domain;
using Infrastructure.Abstractions;
using MediatR;

namespace UseCases;

/// Handler for <inheritdoc cref="CreateFileCommand"/>
internal class CreateFileCommandHandler : AsyncRequestHandler<CreateFileCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public CreateFileCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    protected override async Task Handle(CreateFileCommand request, CancellationToken cancellationToken)
    {
        var file = mapper.Map<StoredFile>(request.FileDto);
        await appDbContext.StoredFiles.AddAsync(file, cancellationToken);
    }
}
