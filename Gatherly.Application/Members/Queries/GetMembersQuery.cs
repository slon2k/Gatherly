using Gatherly.Domain.Shared;
using Gatherly.Domain.Entities;
using MediatR;

namespace Gatherly.Application.Members.Queries
{
    public class GetMembersQuery : IRequest<Result<IEnumerable<Member>>>
    {
    }
}
