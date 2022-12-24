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
            await Task.CompletedTask;
            var member = Member.Create(request.FirstName, request.LastName, request.Email);
            memberRepository.Create(member);
            
            return Result<CreateMemberResponse>.Success(new CreateMemberResponse(member.Id, member.FirstName, member.LastName, member.Email));
        }
        catch (Exception e)
        {
            return Result<CreateMemberResponse>.Failure(Error.Failure(e.Message));
        }
    }
}
