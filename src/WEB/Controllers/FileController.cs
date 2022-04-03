﻿using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    /// <param name="userAccessor">Logged user accessor.</param>
    /// <param name="mapper">Mapper.</param>
    public FileController(IMediator mediator, IUserAccessor userAccessor, IMapper mapper)
    {
        this.mediator = mediator;
        this.userAccessor = userAccessor;
        this.mapper = mapper;
    }

    /// <summary>
    /// POST upload file action.
    /// </summary>
    /// <param name="file">File.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Upload([Required]IFormFile file, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await mediator.Send(new UploadFileCommand(file), cancellationToken);

        return Ok();
    }

    /// <summary>
    /// POST upload text action.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> UploadText([FromQuery][Required] string text, CancellationToken cancellationToken)
    {
        return Ok();
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
        return Ok(mapper.Map<IEnumerable<StoredFileDto>>(items));
    }

    /// <summary>
    /// POST delete file action.
    /// </summary>
    /// <param name="fileId">File ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Status code.</returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Delete(int fileId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteFileCommand(fileId), cancellationToken);
        return Ok();
    }

    /// <summary>
    /// GET download file action.
    /// </summary>
    /// <param name="fileId">File ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>File.</returns>
    [HttpGet]
    public async Task<FileStreamResult> Download(int fileId, CancellationToken cancellationToken)
    {
        var file = await mediator.Send(new DownloadFileQuery(fileId), cancellationToken);
        return File(file.ResponseStream, file.ContentType, file.Name);
    }
}
