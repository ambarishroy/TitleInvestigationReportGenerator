using TIR.SharedKernel.ValueObjects;

namespace TIR.SharedKernel.Events
{
    public sealed record LegalFactsExtractedEvent
    (
        Guid TenantId,
        Guid ProjectId,
        Guid DocumentId,
        IReadOnlyList<LegalFact> Facts,
        DateTime ExtractedAtUtc,
        string CorrelationId
    );
}
