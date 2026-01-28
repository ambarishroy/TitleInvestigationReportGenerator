namespace TIR.DocumentService.Domain
{
    public sealed class Document
    {
        public Guid DocumentId { get; private set; }
        public Guid ProjectId { get; private set; }
        public string FileName { get; private set; }
        public int PageCount { get; private set; }
        public DocumentType Type { get; private set; }
        public DocumentStatus Status { get; private set; }
        public DateTime UploadedAtUtc { get; private set; }
        public string UploadedBy { get; private set; }
        public string Language { get; private set; }
        private Document() { }
        public Document(Guid projectId, string fileName, int pageCount, DocumentType type, string language, string uploadedBy)
        {
            DocumentId = Guid.NewGuid();
            ProjectId = projectId;
            FileName = fileName;
            PageCount = pageCount;
            Type = type;
            UploadedBy = uploadedBy;
            UploadedAtUtc = DateTime.UtcNow;
            Language = language;
            Status = DocumentStatus.Uploaded;
        }
        public void MarkProcessing()
        {
            if (Status != DocumentStatus.Uploaded)
            {
                throw new InvalidOperationException("Document must be in Uploaded state.");
            }
            Status = DocumentStatus.Processing;
        }
        public void MarkActionRequired()
        {
            Status = DocumentStatus.ActionRequired;
        }
        public void MarkProcessed()
        {
            if (Status != DocumentStatus.Processing)
            {
                throw new InvalidOperationException("Processing must complete first.");
            }
            Status = DocumentStatus.Processed;
        }
    }
}
    
