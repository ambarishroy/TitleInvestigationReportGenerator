using System.Text.Json;
using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using TIR.SharedKernel.Events;

namespace TIR.OcrService.Infrastructure
{
    public sealed class EventPublisher
    {
        private readonly IAmazonEventBridge _eventBridge;
        private readonly string _busName;
        public EventPublisher(IAmazonEventBridge eventBridge, IConfiguration config)
        {
            _eventBridge = eventBridge;
            _busName = config["EventBridge:BusName"]
                ?? throw new InvalidOperationException("EventBridge bus name missing.");
        }
        public async Task PublishAsync(OcrCompletedEvent evt,CancellationToken ct)
        {
            var entry = new PutEventsRequestEntry
            {
                Source = "tir.ocr",
                DetailType = nameof(OcrCompletedEvent),
                Detail = JsonSerializer.Serialize(evt),
                EventBusName = _busName,
                Time = evt.ProcessedAtUtc
            };
            await _eventBridge.PutEventsAsync(
               new PutEventsRequest { Entries = new() { entry } },
               ct);
        }
        public void PublishOcrCompleted(Guid documentId, bool isFullyReadable, List<int> failedPages)
        {
            // Test
            Console.WriteLine($"OCR Completed for {documentId}, Readable: {isFullyReadable}");
        }
    }
}
