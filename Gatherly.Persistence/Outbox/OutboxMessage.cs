namespace Gatherly.Persistence.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime OccuredDateTime { get; set; }

        public DateTime? ProcessedDateTime { get; set; }

        public string? Error { get; set; }
    }
}
