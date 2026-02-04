using System.Text.Json;
using Amazon.S3.Model;
using Amazon.EventBridge;
using Amazon.EventBridge.Model;
namespace TIR.DocumentService.Events
{
    public class EventBridgePublisher: IDomainEventPublisher
    {
        private readonly IAmazonEventBridge _eventBridge;
        private readonly string _busName;
        public EventBridgePublisher(IAmazonEventBridge eventBridge, IConfiguration config)
        {
            _eventBridge = eventBridge;
            _busName = config["EventBridge:BusName"];
        }
        public async Task PublishAsync<T>(T domainEvent, string correlationId, CancellationToken ct)
        {
            var entry = new PutEventsRequestEntry
            {
                Source = "tir.document-service",
                DetailType = typeof(T).Name,
                Detail = JsonSerializer.Serialize(domainEvent),
                EventBusName = _busName,
                Time = DateTime.UtcNow
            };
            var response = await _eventBridge.PutEventsAsync(
           new PutEventsRequest { Entries = new() { entry } }, ct);
            if (response.FailedEntryCount > 0)
            {
                throw new InvalidOperationException("EventBridge publish failed.");
            }
        }
    }
}
