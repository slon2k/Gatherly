using Gatherly.Application.Abstractions;

namespace Gatherly.Application.Members.Commands;

public record UpdateMemberCommand(Guid Id, string FirstName, string LastName, string Email) : ICommand<UpdateMemberResponse>;

public record UpdateMemberResponse(Guid Id, string FirstName, string LastName, string Email);
