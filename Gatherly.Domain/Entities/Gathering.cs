using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Entities
{
    public sealed class Gathering : AggregateRoot
    {
        public const int NameMaxLength = 100;
        public const int LocationMaxLength = 100;
        public const int NumberOfAttendeesLimit = 1000;

        private readonly List<Invitation> invitations = new();

        private readonly List<Attendee> attendees = new();

        private Gathering(Guid id, GatheringType type, Member creator, DateTime scheduledDate, string name, string? location) : base(id)
        {
            Type = type;
            CreatorId = creator.Id;
            Creator = creator;
            ScheduledDate = scheduledDate;
            Name = name;
            Location = location;
        }

        public GatheringType Type { get; private set; }

        public Guid CreatorId { get; private set; }

        public DateTime ScheduledDate { get; private set; }

        public string Name { get; private set; }

        public string? Location { get; set; }

        public int NumberOfAttendees => attendees.Count;

        public int? MaxNumberOfAttendees { get; set; }

        public DateTime? InvitationsExpireAt { get; set; }

        public Member Creator { get; private set; }

        public IReadOnlyCollection<Invitation> Invitations => invitations;

        public IReadOnlyCollection<Attendee> Attendees => attendees;

        public static Gathering Create(
            GatheringType type,
            Member creator,
            DateTime scheduledDate,
            string name,
            string? location,
            int? maxNumberOfAttendees,
            int? invitationsValidBeforeInHours)
        {
            var gathering = new Gathering(
                Guid.NewGuid(),
                type,
                creator,
                scheduledDate,
                name,
                location);

            switch (type)
            {
                case GatheringType.AttendeesFixedNumber:
                    if (maxNumberOfAttendees is null || maxNumberOfAttendees < 1)
                    {
                        throw new ArgumentException($"Invalid argument {nameof(maxNumberOfAttendees)}", nameof(maxNumberOfAttendees));
                    }
                    
                    gathering.MaxNumberOfAttendees = maxNumberOfAttendees;
                    break;
                case GatheringType.InvitationsExpiration:
                    if (invitationsValidBeforeInHours is null || maxNumberOfAttendees < 1)
                    {
                        throw new ArgumentException($"Invalid argument {nameof(invitationsValidBeforeInHours)}", nameof(invitationsValidBeforeInHours));
                    }

                    gathering.InvitationsExpireAt = 
                        gathering.ScheduledDate.AddHours(-invitationsValidBeforeInHours.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }


            return gathering;
        }

        public Result<Attendee> AcceptInvitation(Invitation invitation)
        {
            if (IsExpired())
            {
                invitation.Expire();
                
                return Errors.Gathering.Expired;
            }

            var attendee = invitation.Accept();

            RaiseDomainEvent(new InvitationAcceptedDomainEvent(invitation.Id, Id));

            attendees.Add(attendee);

            return attendee;
        }

        public Invitation SendInvitation(Member member)
        {
            var invitation = new Invitation(Guid.NewGuid(), member, this);

            invitations.Add(invitation);

            return invitation;
        }

        public bool IsExpired() => (Type == GatheringType.InvitationsExpiration && InvitationsExpireAt < DateTime.Now) ||
            (Type == GatheringType.AttendeesFixedNumber && NumberOfAttendees >= MaxNumberOfAttendees);
    }
}