using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Members.Commands;

public record CreateMemberCommand(string FirstName, string LastName, string Email) : IRequest<Result<CreateMemberResponse>>;
