using MediatR;
using UseCases.Common;

namespace UseCases;

/// <summary>
/// Get user files query.
/// </summary>
/// <param name="UserId">User ID.</param>
public record GetFilesQuery(int UserId) : IRequest<IEnumerable<StoredFileDto>>;
