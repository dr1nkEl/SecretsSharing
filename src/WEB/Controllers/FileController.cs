using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UseCases;
using WEB.Infrastructure.Models;

namespace WEB.Controllers;

/// <summary>
/// File API controller.
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IUserAccessor userAccessor;
    private readonly IMapper mapper;
    private readonly IDataProtector dataProtector;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="userAccessor">Logged user accessor.</param>
    /// <param name="mapper">Mapper.</param>
    public FileController(IMediator mediator, IUserAccessor userAccessor,
        IMapper mapper, IDataProtectionProvider protectionProvider)
    {
        this.mediator = mediator;
        this.userAccessor = userAccessor;
        this.mapper = mapper;
        this.dataProtector = protectionProvider.CreateProtector("WEB.FileController");
    }

    /// <summary>
    /// POST upload file action.
    /// </summary>
    /// <param name="file">File.</param>
    /// <param name="isDeleting">Is deleting.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Upload([Required]IFormFile file, [FromQuery] bool isDeleting, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator.Send(new UploadFileCommand(file, isDeleting), cancellationToken);
        return Ok(GetDownloadFileLink(dataProtector.Protect(response.Result.ToString())));
    }

    /// <summary>
    /// POST upload text action.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <param name="isDeleting">Is deleting.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> UploadText([FromQuery][Required] string text, [FromQuery]bool isDeleting, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await mediator.Send(new UploadTextCommand(text, isDeleting), cancellationToken);

        return Ok(GetDownloadFileLink(dataProtector.Protect(response.Result.ToString())));
    }

    /// <summary>
    /// GET files of user action.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User files.</returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<StoredFileDto>>> List(CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetMeAsync(cancellationToken);
        var items = await mediator.Send(new GetFilesQuery(user.Id), cancellationToken);
        var mapped = mapper.Map<IEnumerable<StoredFileDto>>(items);
        foreach (var dto in mapped)
        {
            dto.DownloadLink = GetDownloadFileLink(dataProtector.Protect(dto.Id.ToString()));
        }
        return Ok(mapped);
    }

    /// <summary>
    /// POST delete file action.
    /// </summary>
    /// <param name="fileId">File ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost("{fileId}")]
    [Authorize]
    public async Task<ActionResult> Delete(string fileId, CancellationToken cancellationToken)
    {
        var unportected = dataProtector.Unprotect(fileId);

        var parsingResult = int.TryParse(unportected, out int id);

        if (!parsingResult)
        {
            return BadRequest("Invalid ID.");
        }

        await mediator.Send(new DeleteFileCommand(id), cancellationToken);
        return Ok();
    }

    /// <summary>
    /// GET download file action.
    /// </summary>
    /// <param name="fileId">File ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>File.</returns>
    [HttpGet("{fileId}")]
    public async Task<FileStreamResult> Download(string fileId, CancellationToken cancellationToken)
    {
        var unportected = dataProtector.Unprotect(fileId);

        var parsingResult = int.TryParse(unportected, out int id);

        if (!parsingResult)
        {
            throw new ValidationException("Invalid ID.");
        }

        var fileDownloadDto = await mediator.Send(new DownloadFileQuery(id), cancellationToken);

        var file = await mediator.Send(new GetFileQuery(id), CancellationToken.None);
        if (file.IsDeleting)
        {
            await mediator.Send(new DeleteFileCommand(id), CancellationToken.None);
        }
        return File(fileDownloadDto.ResponseStream, fileDownloadDto.ContentType, file.Name);
    }

    private string GetDownloadFileLink(string fileId)
    {
        return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{Url.Action("Download", new { fileId })}";
    }
}
