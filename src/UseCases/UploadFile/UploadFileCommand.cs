using MediatR;
using Microsoft.AspNetCore.Http;
using UseCases.Common;

namespace UseCases;

/// <summary>
/// Upload file command.
/// </summary>
/// <param name="File"></param>
/// <param name="IsDeleting">Is deleting.</param>
public record UploadFileCommand(IFormFile File, bool IsDeleting) : IRequest<Response<int>>;
