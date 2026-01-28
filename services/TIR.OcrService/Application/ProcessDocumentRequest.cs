namespace TIR.OcrService.Application
{
    public sealed class ProcessDocumentRequest
    {
        public Guid DocumentId { get; init; }
        public string FilePath { get; init; } = default!;
        public string LanguageHint { get; init; } = "EN";
    }
}
