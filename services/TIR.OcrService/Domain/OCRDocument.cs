namespace TIR.OcrService.Domain
{
    public sealed class OCRDocument
    {
        public Guid DocumentId { get; private set; }
        public OCRLanguage Language { get; private set; }
        public List<OCRPageResult> Pages { get; private set; } = new();

        public bool IsFullyReadable => Pages.TrueForAll(p => p.IsReadable);
        public OCRDocument(Guid documentId, OCRLanguage language)
        {
            DocumentId = documentId;
            Language = language;
        }
        public void AddPageResult(OCRPageResult page)
        {
            Pages.Add(page);
        }
    }
}
