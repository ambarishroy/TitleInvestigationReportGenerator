using TIR.SharedKernel.ValueObjects;
namespace TIR.LegalExtractionService.Application
{
    public sealed class ExtractLegalFactsResponse
    {
        public List<LegalFact> Facts { get; init; } = new();
        public Guid DocumentId { get; init; }
    }
}
