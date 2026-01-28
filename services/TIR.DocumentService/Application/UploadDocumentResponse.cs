namespace TIR.DocumentService.Application
{
    public class UploadDocumentResponse
    {
        public Guid DocumentId { get; init; }
        public string Status { get; init; } = default!;

    }
}
