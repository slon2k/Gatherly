using Gatherly.Application.Abstractions;

namespace Gatherly.Application.Members.Commands;

public record CreateMemberCommand(string FirstName, string LastName, string Email) : ICommand<CreateMemberResponse>;

public record CreateMemberResponse(Guid Id, string FirstName, string LastName, string Email);
