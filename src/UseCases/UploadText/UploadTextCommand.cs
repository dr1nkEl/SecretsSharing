using MediatR;
using UseCases.Common;

namespace UseCases;

/// <summary>
/// Upload text command.
/// </summary>
/// <param name="Text">Text.</param>
/// <param name="IsDeleting">Is deleting.</param>
public record UploadTextCommand(string Text, bool IsDeleting) : IRequest<Response<int>>;
