namespace Gatherly.Application.Members.Commands
{
    public record CreateMemberResponse(Guid id, string FirstName, string LastName, string Email);
}
