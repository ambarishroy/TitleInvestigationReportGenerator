namespace TIR.OcrService.Infrastructure
{
    public sealed class EventPublisher
    {
        public void PublishOcrCompleted(Guid documentId, bool isFullyReadable, List<int> failedPages)
        {
            // TODO: send SQS / EventBridge event
            Console.WriteLine($"OCR Completed for {documentId}, Readable: {isFullyReadable}");
        }
    }
}
