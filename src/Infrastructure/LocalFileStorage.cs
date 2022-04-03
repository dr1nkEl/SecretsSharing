using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Saritasa.Tools.Domain.Exceptions;
using UseCases;
using UseCases.Common;

namespace Infrastructure;

/// <summary>
/// Local file storage.
/// </summary>
public class LocalFileStorage : IFileStorage
{
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IHostingEnvironment environment;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="loggedUserAccessor">Logged user accessor.</param>
    /// <param name="environment">Hosting environment.</param>
    /// <param name="mapper">Mapper.</param>
    /// <param name="mediator">Mediator.</param>
    public LocalFileStorage(ILoggedUserAccessor loggedUserAccessor, IHostingEnvironment environment, IMediator mediator, IMapper mapper)
    {
        this.loggedUserAccessor = loggedUserAccessor;
        this.environment = environment;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public Task DeleteFileAsync(string fileId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task DownloadAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task UplaodAsync(IFormFile file, CancellationToken cancellationToken)
    {
        if (file is null)
        {
            throw new ValidationException("File can not be null.");
        }

        var user = await loggedUserAccessor.GetMeAsync(cancellationToken);

        var path = $"/Files/{user.UserName}/{file.FileName}";

        using var fileStream = new FileStream($"{environment.WebRootPath}{path}", FileMode.Create);

        await file.CopyToAsync(fileStream, cancellationToken);

        var fileDto = new StoredFileDto
        {
            AssociatedUserId = user.Id,
            Name = file.FileName,
        };

        await mediator.Send(new CreateFileCommand(fileDto), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UploadTextAsync(string text, CancellationToken cancellationToken)
    {
        var path = GetTempFilePath(text);

        using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
        
        

        throw new NotImplementedException();
    }

    private static string GetTempFilePath(string text)
    {
        var path = Path.GetTempFileName();
        Path.ChangeExtension(path, ".txt");
        File.WriteAllText(path, text);
        return path;
    }

}
