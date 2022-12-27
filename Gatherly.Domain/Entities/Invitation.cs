using Gatherly.Domain.Enums;
using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities
{
    public class Invitation : Entity, IAuditableEntity
    {
        public Guid MemberId { get; private set; }

        public Guid GatheringId { get; private set; }

        public InvitationStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public virtual Member Member { get; private set; }

        public virtual Gathering Gathering { get; private set; }

        private Invitation(Guid id) : base(id)
        {
            Status = InvitationStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        internal Invitation(Guid id, Member member, Gathering gathering) : base(id)
        {
            Status = InvitationStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            Member = member;
            MemberId = member.Id;
            GatheringId = gathering.Id;
            Gathering = gathering;
        }

        internal Attendee Accept()
        {
            Status = InvitationStatus.Accepted;
            UpdatedAt= DateTime.UtcNow;

            var attendee = new Attendee(Guid.NewGuid(), this);

            return attendee;
        }

        internal void Expire()
        {
            Status = InvitationStatus.Expired;
            UpdatedAt= DateTime.UtcNow;
        }
    }
}
