namespace TIR.AuditService.Domain
{
    public sealed class FinalTir
    {
        public Guid ProjectId { get; private set; }
        public string FileName { get; private set; } = default!;
        public string UploadedByUserId { get; private set; } = default!;
        public DateTime UploadedAtUtc { get; private set; }
        public int VersionNumber { get; private set; }
        public string Status { get; private set; } = "UnderReview"; // initial
        
        public FinalTir(Guid projectId, string fileName, string uploadedByUserId, int versionNumber)
        {
            ProjectId = projectId;
            FileName = fileName;
            UploadedByUserId = uploadedByUserId;
            UploadedAtUtc = DateTime.UtcNow;
            VersionNumber = versionNumber;
        }
        public void MarkReportGenerated()
        {
            Status = "ReportGenerated";
        }
    }
}
