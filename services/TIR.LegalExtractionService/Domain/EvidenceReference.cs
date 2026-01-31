namespace TIR.LegalExtractionService.Domain
{
    public sealed class EvidenceReference
    {
        public Guid DocumentId { get; private set; }
        public int PageNumber { get; private set; }
        public float OcrConfidence { get; private set; }
        public EvidenceReference(Guid documentId, int pageNumber, float confidence)
        {
            DocumentId = documentId;
            PageNumber = pageNumber;
            OcrConfidence = confidence;
        }
    }
}
