using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Errors;

public static class Gathering
{
    public static Error Expired => Error.Failure("Gathering.InvitationExpired", "The invitation is expired");
}   
