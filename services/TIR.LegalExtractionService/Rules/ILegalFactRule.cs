using TIR.SharedKernel.ValueObjects;

namespace TIR.LegalExtractionService.Rules
{
    public interface ILegalFactRule
    {
        IEnumerable<LegalFact> Apply(Guid documentId, int pageNumber, string text);
    }
}
