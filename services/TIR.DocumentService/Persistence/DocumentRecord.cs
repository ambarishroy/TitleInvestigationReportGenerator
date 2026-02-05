using TIR.DocumentService.Domain;
using TIR.SharedKernel.Enums;

namespace TIR.DocumentService.Persistence
{
    public sealed class DocumentRecord
    {
        public Guid DocumentId { get; set; }
        public Guid ProjectId { get; set; }
        public string FileName { get; set; } = default!;
        public int PageCount { get; set; }
        public DocumentType Type { get; set; }
        public DocumentStatus Status { get; set; }
        public string Language { get; set; } = default!;
        public string UploadedBy { get; set; } = default!;
        public DateTime UploadedAtUtc { get; set; }
        public string Sha256Hash { get; set; } = default!;
        public void ApplyStatus(DocumentStatus status)
        {
            Status = status;
        }
    }
}
