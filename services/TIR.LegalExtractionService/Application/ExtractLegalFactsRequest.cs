namespace TIR.LegalExtractionService.Application
{
    public sealed class ExtractLegalFactsRequest
    {
        public Guid DocumentId { get; init; }
        public Dictionary<int, string> Pages { get; init; } = new();
    }
}
