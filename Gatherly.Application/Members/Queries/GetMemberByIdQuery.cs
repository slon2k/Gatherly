using Gatherly.Application.Abstractions;

namespace Gatherly.Application.Members.Queries;

public record GetMemberByIdQuery(Guid Id) : IQuery<MemberResponse>;
