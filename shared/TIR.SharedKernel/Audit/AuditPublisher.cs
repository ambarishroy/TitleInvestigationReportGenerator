using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using System.Text.Json;
//using TIR.DocumentService.Audit;
using TIR.SharedKernel.Events;
using Microsoft.Extensions.Configuration;

namespace TIR.SharedKernel.Audit
{
    public sealed class AuditPublisher : IAuditPublisher
    {
        private readonly IAmazonEventBridge _eventBridge;
        private readonly string _busName;
       

        public AuditPublisher(
            IAmazonEventBridge eventBridge,
            IConfiguration configuration)
        {
            _eventBridge = eventBridge;
            _busName = configuration["EventBridge:BusName"]
                       ?? throw new InvalidOperationException("EventBridge bus name not configured.");
        }

        public async Task PublishAsync(
            AuditEvent auditEvent,
            CancellationToken ct)
        {
            var entry = new PutEventsRequestEntry
            {
                Source = "tir.audit",
                DetailType = "AuditEvent",
                Detail = JsonSerializer.Serialize(auditEvent),
                EventBusName = _busName,
                Time = auditEvent.TimestampUtc
            };

            var response = await _eventBridge.PutEventsAsync(
                new PutEventsRequest
                {
                    Entries = new List<PutEventsRequestEntry> { entry }
                },
                ct);

            if (response.FailedEntryCount > 0)
            {
                var error = response.Entries.FirstOrDefault(e => e.ErrorCode != null);
                throw new InvalidOperationException(
                    $"Audit event publishing failed: {error?.ErrorMessage}");
            }
        }
    }
}
