using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Members.Queries;

public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, Result<IEnumerable<Member>>>
{
    private readonly IMemberRepository memberRepository;

    public GetMembersQueryHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task<Result<IEnumerable<Member>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
		try
		{
            var result = await memberRepository.GetAllAsync(cancellationToken);

            return result.ToList();
		}
		catch (Exception e)
		{
			return Error.Failure("CreateMember.Failure", e.Message);
		}
    }
}
