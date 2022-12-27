using Gatherly.Application.Abstractions;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using static Gatherly.Domain.Errors.Member;

namespace Gatherly.Application.Members.Queries;

internal class GetMemberByIdQueryHandler : IQueryHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task<Result<MemberResponse>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var member = await memberRepository.FindAsync(request.Id, cancellationToken);
            
            if (member is null)
            {
                return NotFound;
            }

            return new MemberResponse(
                member.Id,
                member.FirstName,
                member.LastName,
                member.Email);
        }
        catch (Exception e)
        {
            return Error.Failure("Member.GetById", e.Message);
        }
    }
}
