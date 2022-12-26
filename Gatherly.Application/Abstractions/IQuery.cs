using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
