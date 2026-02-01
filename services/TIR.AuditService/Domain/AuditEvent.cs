namespace TIR.AuditService.Domain
{
    public sealed class AuditEvent
    {
        public Guid EventId { get; private set; }
        public string EventType { get; private set; } = default!;
        public Guid ProjectId { get; private set; }
        public string UserId { get; private set; } = default!;
        public DateTime TimestampUtc { get; private set; }
        public string Details { get; private set; } = default!;

        public AuditEvent(string eventType, Guid projectId, string userId, string details)
        {
            EventId = Guid.NewGuid();
            EventType = eventType;
            ProjectId = projectId;
            UserId = userId;
            Details = details;
            TimestampUtc = DateTime.UtcNow;
        }
    }
}
