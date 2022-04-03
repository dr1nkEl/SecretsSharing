using MediatR;

namespace UseCases;

/// <summary>
/// Upload text command.
/// </summary>
/// <param name="Text">Text.</param>
public record UploadTextCommand(string Text) : IRequest;
