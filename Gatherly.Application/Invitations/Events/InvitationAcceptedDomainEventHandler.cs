using Gatherly.Application.Abstractions;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Repositories;
using MediatR;

namespace Gatherly.Application.Invitations.Events;

public sealed class InvitationAcceptedDomainEventHandler : INotificationHandler<InvitationAcceptedDomainEvent>
{
    private readonly IEmailService emailService;

    private readonly IGatheringRepository gatheringRepository;

    public InvitationAcceptedDomainEventHandler(IEmailService emailService, IGatheringRepository gatheringRepository)
    {
        this.emailService = emailService;
        this.gatheringRepository = gatheringRepository;
    }

    public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var gathering = gatheringRepository.GetById(notification.GatheringId);

        if (gathering == null)
        {
            return;
        }

        var invitation = gathering.Invitations.FirstOrDefault(i => i.Id == notification.InvitationId);

        if (invitation == null)
        {
            return;
        }

        await emailService.SendInvitationEmailAsync(invitation, cancellationToken);
    }
}
