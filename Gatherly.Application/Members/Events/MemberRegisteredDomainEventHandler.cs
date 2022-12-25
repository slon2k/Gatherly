using Gatherly.Application.Abstractions;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using MediatR;

namespace Gatherly.Application.Members.Events
{
    public class MemberRegisteredDomainEventHandler : INotificationHandler<MemberRegisteredDomainEvent>
    {
        private readonly IMemberRepository memberRepository;

        private readonly IEmailService emailService;

        public MemberRegisteredDomainEventHandler(IMemberRepository memberRepository, IEmailService emailService)
        {
            this.memberRepository = memberRepository;
            this.emailService = emailService;
        }

        public async Task Handle(MemberRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            if (await memberRepository.FindAsync(notification.MemberId, cancellationToken) is Member member)
            {
                await emailService.SendWelcomeEmailAsync(member, cancellationToken);
            }
        }
    }
}
