using MediatR;
using Microsoft.AspNetCore.Http;

namespace UseCases;

/// <summary>
/// Upload file command.
/// </summary>
/// <param name="File"></param>
public record UploadFileCommand(IFormFile File) : IRequest;
