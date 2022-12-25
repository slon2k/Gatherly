using Gatherly.Domain.Entities;

namespace Gatherly.Application.Abstractions;

public interface IEmailService
{
    Task SendInvitationEmailAsync(Invitation invitation, CancellationToken cancellationToken = default);

    Task SendWelcomeEmailAsync(Member member, CancellationToken cancellationToken = default);

}
