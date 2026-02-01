using TIR.AuditService.Domain;

namespace TIR.AuditService.Infrastructure
{
    public sealed class AuditPublisher
    {
        // TODO: AWS EventBridge / SQS publishing
        public void Publish(AuditEvent auditEvent)
        {
            // Immutable storage (DynamoDB / PostgreSQL)
            Console.WriteLine($"Audit Event: {auditEvent.EventType} | ProjectId: {auditEvent.ProjectId} | User: {auditEvent.UserId} | Timestamp: {auditEvent.TimestampUtc}");
        }
        public void PublishFinalTirUploaded(FinalTir finalTir)
        {
            var auditEvent = new AuditEvent(
                eventType: "FinalTirUploaded",
                projectId: finalTir.ProjectId,
                userId: finalTir.UploadedByUserId,
                details: $"FileName={finalTir.FileName}, Version={finalTir.VersionNumber}"
            );

            Publish(auditEvent); 
        }
    }
}
