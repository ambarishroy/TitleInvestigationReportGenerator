using TIR.LegalExtractionService.Application;
using TIR.LegalExtractionService.Rules;
using TIR.SharedKernel.ValueObjects;

namespace TIR.LegalExtractionService.Extractors
{
    public sealed class FactExtractor
    {
        private readonly IEnumerable<ILegalFactRule> _rules;

        public FactExtractor(IEnumerable<ILegalFactRule> rules)
        {
            _rules = rules;
        }

        public List<LegalFact> Extract(ExtractLegalFactsRequest request)
        {
            var facts = new List<LegalFact>();

            foreach (var page in request.Pages)
            {
                var pageNo = page.Key;
                var text = page.Value;

                foreach (var rule in _rules)
                {
                    var extracted = rule.Apply(
                        request.DocumentId,
                        pageNo,
                        text);

                    facts.AddRange(extracted);
                }
            }

            return facts;
        }
    }
}
