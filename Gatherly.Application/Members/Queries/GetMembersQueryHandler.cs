using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Members.Queries;

public class GetMembersQueryHandler : IQueryHandler<GetMembersQuery, IEnumerable<MemberResponse>>
{
    private readonly IMemberRepository memberRepository;

    public GetMembersQueryHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task<Result<IEnumerable<MemberResponse>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
		try
		{
            var members = await memberRepository.GetAllAsync(cancellationToken);

            return members.Select(m => new MemberResponse(m.Id, m.FirstName, m.LastName, m.Email)).ToList();
		}
		catch (Exception e)
		{
			return Error.Failure("CreateMember.Failure", e.Message);
		}
    }
}
