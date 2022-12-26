﻿using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Abstractions;

public interface ICommand : IRequest<Result> 
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
