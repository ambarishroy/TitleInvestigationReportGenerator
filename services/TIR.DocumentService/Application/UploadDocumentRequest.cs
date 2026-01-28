using TIR.DocumentService.Domain;

namespace TIR.DocumentService.Application
{
    public class UploadDocumentRequest
    {
        public Guid ProjectId {get; init;}
        public string FileName { get; init; } = default!;
        public int PageCount { get; init; }
        public DocumentType Type { get; init; }
        public string Language { get; init; } = "EN";
        public string UploadedBy { get; init; } = default!;
    }
}
