using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Common;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Saritasa.Tools.Domain.Exceptions;
using System.Net;
using UseCases;

namespace Infrastructure;

/// <summary>
/// Local file storage.
/// </summary>
public class LocalFileStorage : IFileStorage
{
    private readonly IUserAccessor userAccessor;
    private readonly IHostingEnvironment environment;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userAccessor">Logged user accessor.</param>
    /// <param name="environment">Hosting environment.</param>
    /// <param name="mapper">Mapper.</param>
    /// <param name="mediator">Mediator.</param>
    public LocalFileStorage(IUserAccessor userAccessor, IHostingEnvironment environment,
        IMediator mediator, IMapper mapper)
    {
        this.userAccessor = userAccessor;
        this.environment = environment;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public Task DeleteAsync(int fileId, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task<DownloadFileDto> DownloadAsync(int id, CancellationToken cancellationToken)
    {
        var file = await mediator.Send(new GetFileQuery(id), cancellationToken);
        var user = await userAccessor.Get(file.AssociatedUserId, cancellationToken);
        var path = @$"{environment.WebRootPath}\Files\{user.UserName}\{file.Name}";

        return new DownloadFileDto
        {
            ResponseStream = File.Open(path, FileMode.Open),
            ContentType = "application/octet-stream",
            Name = file.Name,
        };
    }

    /// <inheritdoc/>
    public async Task<Response<StoredFileDto>> UploadAsync(IFormFile file, CancellationToken cancellationToken)
    {
        if (file is null)
        {
            throw new ValidationException("File can not be null.");
        }

        var user = await userAccessor.GetMeAsync(cancellationToken);

        var path = $"/Files/{user.UserName}/{file.FileName}";
        Directory.CreateDirectory($"{environment.WebRootPath}/Files/{user.UserName}");
        using var fileStream = new FileStream($"{environment.WebRootPath}{path}", FileMode.Create, FileAccess.ReadWrite);

        await file.CopyToAsync(fileStream, cancellationToken);

        return new Response<StoredFileDto>
        {
            Result = new StoredFileDto
            {
                AssociatedUserId = user.Id,
                Name = file.FileName,
            }
        };
    }

    /// <inheritdoc/>
    public async Task<Response<StoredFileDto>> UploadTextAsync(string text, CancellationToken cancellationToken)
    {
        var path = GetTempFilePath(text);
        var user = await userAccessor.GetMeAsync(cancellationToken);
        var newFilePath = $@"{environment.WebRootPath}\Files\{user.UserName}";
        Directory.CreateDirectory(newFilePath);
        var fileName = $"{Guid.NewGuid()}.txt";
        using var file = new FileStream(path, FileMode.Open, FileAccess.Read);

        using var fileStream = new FileStream($@"{newFilePath}\{fileName}", FileMode.Create, FileAccess.ReadWrite);

        await file.CopyToAsync(fileStream, cancellationToken);

        return new Response<StoredFileDto>
        {
            Result = new StoredFileDto
            {
                AssociatedUserId = user.Id,
                Name = fileName,
            }
        };
    }

    private static string GetTempFilePath(string text)
    {
        var path = Path.GetTempFileName();
        Path.ChangeExtension(path, ".txt");
        File.WriteAllText(path, text);
        return path;
    }
}
