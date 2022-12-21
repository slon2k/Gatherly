namespace Gatherly.Domain.Entities
{
    public class Attendee
    {
        public Guid MemderId { get; set; }

        public Guid GatheringId { get; set; }

        public Member Member { get; set; }

        public Gathering Gathering { get; set; }
    }
}
