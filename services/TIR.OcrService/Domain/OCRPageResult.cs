namespace TIR.OcrService.Domain
{
    public sealed class OCRPageResult
    {
        public int PageNumber { get; private set; }
        public float Confidence { get; private set; }
        public string ExtractedText { get; private set; } = string.Empty;
        public bool IsReadable => Confidence >= 0.85f;
        public OCRPageResult(int pageNumber, float confidence, string extractedText)
        {
            PageNumber = pageNumber;
            Confidence = confidence;
            ExtractedText = extractedText;
        }
    }
}
