namespace TIR.AuditService.Application
{
    public sealed class UploadFinalTirRequest
    {
        public Guid ProjectId { get; init; }
        public string FileName { get; init; } = default!;
        public byte[] FileContent { get; init; } = default!;
        public string UploadedByUserId { get; init; } = default!;
    }
}
