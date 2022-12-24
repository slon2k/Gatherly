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
		await Task.CompletedTask;
		
		try
		{
			return Result<IEnumerable<Member>>.Success(memberRepository.GetAll());
		}
		catch (Exception e)
		{
			return Result<IEnumerable<Member>>.Failure(Error.Failure("CreateMember.Failure", e.Message));
		}
    }
}
