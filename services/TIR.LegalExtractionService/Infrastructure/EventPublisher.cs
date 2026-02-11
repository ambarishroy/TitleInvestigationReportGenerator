using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using System.Text.Json;
using TIR.SharedKernel.Events;
using TIR.SharedKernel.ValueObjects;

namespace TIR.LegalExtractionService.Infrastructure
{
    public sealed class EventPublisher
    {
        private readonly IAmazonEventBridge _eventBridge;
        private readonly string _busName;

        public EventPublisher(
            IAmazonEventBridge eventBridge,
            IConfiguration config)
        {
            _eventBridge = eventBridge;
            _busName = config["EventBridge:BusName"]!;
        }

        public async Task PublishFactsExtractedAsync(OcrCompletedEvent sourceEvent, List<LegalFact> facts, CancellationToken ct)
        {
            var evt = new LegalFactsExtractedEvent(
                sourceEvent.TenantId,
                sourceEvent.ProjectId,
                sourceEvent.DocumentId,
                facts,
                DateTime.UtcNow,
                sourceEvent.CorrelationId
            );

            var entry = new PutEventsRequestEntry
            {
                Source = "tir.legal-extraction",
                DetailType = nameof(LegalFactsExtractedEvent),
                Detail = JsonSerializer.Serialize(evt),
                EventBusName = _busName
            };

            await _eventBridge.PutEventsAsync(
                new PutEventsRequest
                {
                    Entries = new() { entry }
                },
                ct);
        }
    }
}
