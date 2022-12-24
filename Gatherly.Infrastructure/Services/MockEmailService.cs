using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;

namespace Gatherly.Infrastructure.Services;

public class MockEmailService : IEmailService
{
    private ILogger logger;
    public Task SendInvitationEmailAsync(Invitation invitation, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
