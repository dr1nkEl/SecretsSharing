using MediatR;
using UseCases.Common;

namespace UseCases;

/// <summary>
/// Get file query.
/// </summary>
/// <param name="Id">ID.</param>
public record GetFileQuery(int Id) : IRequest<StoredFileDto>;
