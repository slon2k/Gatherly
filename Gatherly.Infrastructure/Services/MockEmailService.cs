using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;

namespace Gatherly.Infrastructure.Services;

public class MockEmailService : IEmailService
{
    public async Task SendInvitationEmailAsync(Invitation invitation, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        if (invitation is not null && invitation.Member is not null)
        {
            Console.WriteLine("Invitation is sent to " + invitation.Member.Email);
        }
    }

    public async Task SendWelcomeEmailAsync(Member member, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        if (member is not null)
        {
            Console.WriteLine("Invitation is sent to " + member.Email);
        }
    }
}
