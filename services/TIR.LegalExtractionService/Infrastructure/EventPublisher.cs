using TIR.SharedKernel.ValueObjects;
namespace TIR.LegalExtractionService.Infrastructure
{
    public sealed class EventPublisher
    {
        public void PublishFactsExtracted(Guid documentId, List<LegalFact> facts)
        {
            //TODO: In production: send to EventBridge / SQS
            Console.WriteLine($"Facts extracted for Document {documentId}, Count: {facts.Count}");
        }
    }
}
