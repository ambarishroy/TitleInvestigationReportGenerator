using TIR.SharedKernel.ValueObjects;

namespace TIR.ReportDraftService.Domain
{
    public sealed class DraftTir
    {
        public Guid ProjectId { get; private set; }
        public string Filename { get; private set; } = default!;
        public DateTime GeneratedAtUtc { get; private set; }
        public List<LegalFact> Facts { get; private set; } = new();
        public List<EvidenceReference> EvidenceReferences { get; private set; } = new();
        public string Status { get; private set; } = "UnderReview";
        public DraftTir(Guid projectId, string fileName, List<LegalFact> facts, List<EvidenceReference> evidence )
        {
            ProjectId = projectId;
            Filename = fileName;
           
            Facts = facts;
            EvidenceReferences = evidence;
            GeneratedAtUtc = DateTime.UtcNow;
        }
        public void MarkAsFinalized()
        {
            Status = "DraftGenerated";
        }
    }
}
