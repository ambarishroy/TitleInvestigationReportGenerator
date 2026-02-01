namespace TIR.AuditService.Application
{
    public class UploadFinalTirResponse
    {
        public Guid ProjectId { get; init; }
        public int VersionNumber { get; init; }
        public string Status { get; init; } = default!;
    }
}
