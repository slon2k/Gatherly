namespace Gatherly.Domain.Entities
{
    public class Member
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        private readonly List<Invitation> invitations = new();

        private readonly List<Attendee> attendees = new();
        
        public IReadOnlyCollection<Invitation> Invitations => invitations;

        public IReadOnlyCollection<Attendee> Attendees => attendees;
    }
}
