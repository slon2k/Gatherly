using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities
{
    public class Attendee : Entity, IAuditableEntity
    {
        public Guid MemberId { get; private set; }

        public Guid GatheringId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }
        
        public virtual Member? Member { get; private set; }

        public virtual Gathering? Gathering { get; private set; }

        internal Attendee(Guid id, Invitation invitation) : base(id)
        {
            if (invitation is null)
            {
                throw new ArgumentNullException(nameof(invitation));
            }

            Gathering = invitation.Gathering;
            Member = invitation.Member;
            GatheringId = invitation.GatheringId;
            MemberId = invitation.MemberId;
            CreatedAt = DateTime.UtcNow;
        }

        private Attendee(Guid id) : base(id)
        {
        }
    }
}
