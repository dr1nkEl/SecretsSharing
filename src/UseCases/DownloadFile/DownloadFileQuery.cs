﻿using Infrastructure.Common;
using MediatR;

namespace UseCases;

/// <summary>
/// Download file query.
/// </summary>
/// <param name="Id">Id.</param>
public record DownloadFileQuery(int Id) : IRequest<DownloadFileDto>;
