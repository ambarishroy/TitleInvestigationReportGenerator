namespace TIR.LegalExtractionService.Domain
{
    public sealed class LegalFact
    {
        public FactType Type { get; private set; }
        public EvidenceReference Evidence { get; private set; }
        public string Value { get; private set; } = "UNKNOWN";
        public LegalFact(FactType type, EvidenceReference evidence, string value)
        {
            Type = type;
            Evidence = evidence;
            Value = string.IsNullOrWhiteSpace(value) ? "UNKNOWN" : value;
        }
    }
}
