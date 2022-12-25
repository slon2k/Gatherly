using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities
{
    public class Attendee : Entity
    {
        public Guid MemberId { get; private set; }

        public Guid GatheringId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public Member Member { get; private set; }

        public Gathering Gathering { get; private set; }

        private Attendee(Guid id, Member member, Gathering gathering) : base(id)
        {
            Member = member;
            Gathering = gathering;
            GatheringId = gathering.Id;
            MemberId = member.Id;
            CreatedAt = DateTime.UtcNow;
        }

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
    }
}
