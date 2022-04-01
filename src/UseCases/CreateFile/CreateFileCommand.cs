using MediatR;
using UseCases.Common;

namespace UseCases;

/// <summary>
/// Create file command.
/// </summary>
/// <param name="FileDto">File DTO.</param>
public record CreateFileCommand(StoredFileDto FileDto) : IRequest;
