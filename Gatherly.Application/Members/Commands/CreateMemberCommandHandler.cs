using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using MediatR;

namespace Gatherly.Application.Members.Commands;

internal class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<CreateMemberResponse>>
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
            if (memberRepository.GetByEmail(request.Email) is not null)
            {
                return Error.Conflict("CreateMember.DuplicateEmail", "Email is not unique");
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
