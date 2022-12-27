using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities
{
    public class Member : AggregateRoot, IAuditableEntity
    {
        public const int FirstNameMaxLength = 100;
        
        public const int LastNameMaxLength = 100;

        public const int EmailMaxLength = 50;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; }

        public DateTime? UpdatedAt { get; }

        private readonly List<Invitation> invitations = new();

        private readonly List<Attendee> attendees = new();

        private readonly List<Gathering> gatherings = new();

        public IReadOnlyCollection<Invitation> Invitations => invitations;

        public IReadOnlyCollection<Attendee> Attendees => attendees;

        public IReadOnlyCollection<Gathering> GatheringsCreated => gatherings;

        private Member(Guid id, string firstName, string lastName, string email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt= DateTime.Now;
        }

        public static Member Create(string firstName, string lastName, string email)
        {
            var member = new Member(Guid.NewGuid(), firstName, lastName, email);

            member.RaiseDomainEvent(new MemberRegisteredDomainEvent(member.Id));

            return member;
        }
    }
}
