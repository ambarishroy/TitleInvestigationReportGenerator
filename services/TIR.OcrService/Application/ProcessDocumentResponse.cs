namespace TIR.OcrService.Application
{
    public class ProcessDocumentResponse
    {
        public Guid DocumentId { get; init; }
        public bool IsFullyReadable { get; init; }
        public List<int> FailedPages { get; init; } = new();
    }
}
