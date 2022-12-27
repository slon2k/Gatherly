using Gatherly.Application.Abstractions;

namespace Gatherly.Application.Members.Queries;

public record GetMemberByIdQuery(Guid Id) : IQuery<MemberResponse>;

public record MemberResponse(Guid Id, string FirstName, string LastName, string Email);

