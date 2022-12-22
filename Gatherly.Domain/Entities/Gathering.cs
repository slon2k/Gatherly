﻿namespace Gatherly.Domain.Entities
{
    public class Gathering
    {
        private readonly List<Invitation> invitations = new();

        private readonly List<Attendee> attendees = new();

        private Gathering(Guid id, GatheringType type, Member creator, DateTime scheduledDate, string name, string? location)
        {
            Id = id;
            Type = type;
            CreatorId = creator.Id;
            Creator = creator;
            ScheduledDate = scheduledDate;
            Name = name;
            Location = location;
        }

        public Guid Id { get; private set; }

        public GatheringType Type { get; private set; }

        public Guid CreatorId { get; private set; }

        public Member Creator { get; private set; }

        public DateTime ScheduledDate { get; private set; }

        public string Name { get; private set; }

        public string? Location { get; set; }

        public int NumberOfAttendees { get; private set; }

        public int? MaxNumberOfAttendees { get; set; }

        public DateTime? InvitationsExpireAt { get; set; }

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

        public Attendee AcceptInvitation(Invitation invitation)
        {
            var attendee = invitation.Accept();

            attendees.Add(attendee);

            NumberOfAttendees++;

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