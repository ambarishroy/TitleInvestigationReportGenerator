using TIR.SharedKernel.Enums;
using TIR.SharedKernel.ValueObjects;

namespace TIR.ReportDraftService.Domain
{
    public sealed class TirProjectSummary
    {
        public string ProjectName { get; init; } = default!;
        public string BankTenantId { get; init; } = default!;
        public string JurisdictionCode { get; init; } = default!;
        public LoanType LoanType { get; init; }
        public List<LegalFact> Facts { get; init; } = new();
        public List<EvidenceReference> Evidence { get; init; } = new();
        public List<(string FileName, DocumentType Type)> Documents { get; init; } = new();
        public OwnershipTimeline Timeline { get; init; } = new();
        public List<RiskItem> Risks { get; init; } = new();
    }
}
