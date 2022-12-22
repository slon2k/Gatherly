namespace Gatherly.Domain.Entities
{
    public class Invitation
    {
        internal Invitation(Guid id, Member member, Gathering gathering)
        {
            Id = id;
            Status = InvitationStatus.Pending;
            CreatedAt= DateTime.UtcNow;
            Member = member;
            MemberId= member.Id;
            GatheringId= gathering.Id;
            Gathering = gathering;
        }

        public Guid Id { get; private set; }

        public Guid MemberId { get; private set; }

        public Guid GatheringId { get; private set; }

        public InvitationStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public Member Member { get; private set; }

        public Gathering Gathering { get; private set; }

        internal Attendee Accept()
        {
            Status = InvitationStatus.Accepted;
            UpdatedAt= DateTime.UtcNow;

            var attendee = new Attendee(this);

            return attendee;
        }

        internal void Expire()
        {
            Status = InvitationStatus.Expired;
            UpdatedAt= DateTime.UtcNow;
        }
    }
}
