using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using static Gatherly.Domain.Errors.Member;

namespace Gatherly.Application.Members.Commands;

internal class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, CreateMemberResponse>
{
    private readonly IMemberRepository memberRepository;

    public CreateMemberCommandHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task<Result<CreateMemberResponse>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await memberRepository.GetByEmailAsync(request.Email) is not null)
            {
                return DuplicateEmail;
            }

            var member = Member.Create(request.FirstName, request.LastName, request.Email);
            
            await memberRepository.AddAsync(member, cancellationToken);

            await memberRepository.SaveChangesAsync(cancellationToken);
            
            return new CreateMemberResponse(member.Id, member.FirstName, member.LastName, member.Email);
        }
        catch (Exception e)
        {
            return Error.Failure("CreateMember.Failure", e.Message);
        }
    }
}
