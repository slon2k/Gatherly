using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities
{
    public class Member : AggregateRoot
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        private readonly List<Invitation> invitations = new();

        private readonly List<Attendee> attendees = new();

        public IReadOnlyCollection<Invitation> Invitations => invitations;

        public IReadOnlyCollection<Attendee> Attendees => attendees;

        public Member(Guid id, string firstName, string lastName, string email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
