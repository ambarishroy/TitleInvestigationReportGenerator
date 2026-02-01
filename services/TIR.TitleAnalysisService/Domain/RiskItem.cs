using TIR.SharedKernel.ValueObjects;

namespace TIR.TitleAnalysisService.Domain
{
    public sealed class RiskItem
    {
        public string Description { get; private set; } = string.Empty;
        public EvidenceReference Evidence { get; private set; }

        public RiskItem(string description, EvidenceReference evidence)
        {
            Description = description;
            Evidence = evidence;
        }
    }
}
