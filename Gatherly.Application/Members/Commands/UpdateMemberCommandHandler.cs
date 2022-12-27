using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using static Gatherly.Domain.Errors.Member;

namespace Gatherly.Application.Members.Commands;

internal class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand, UpdateMemberResponse>
{
    private readonly IMemberRepository memberRepository;

    private readonly IUnitOfWork unitOfWork;

    public UpdateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        this.memberRepository = memberRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateMemberResponse>> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await memberRepository.FindAsync(request.Id, cancellationToken) is not Member member)
            {
                return NotFound;
            }

            if (request.Email.ToLower() != member.Email.ToLower() && await memberRepository.GetByEmailAsync(request.Email, cancellationToken) is not null)
            {
                return DuplicateEmail;
            }

            member.FirstName = request.FirstName;
            member.LastName = request.LastName;
            member.Email = request.Email;
            
            memberRepository.Update(member);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return new UpdateMemberResponse(member.Id, member.FirstName, member.LastName, member.Email);
        }
        catch (Exception e)
        {
            return Error.Failure("CreateMember.Failure", e.Message);
        }
    }
}
