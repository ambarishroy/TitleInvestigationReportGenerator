using System.Text.RegularExpressions;
using TIR.SharedKernel.Enums;
using TIR.SharedKernel.ValueObjects;

namespace TIR.LegalExtractionService.Rules
{
    public sealed class BorrowerNameRule : ILegalFactRule
    {
        private static readonly Regex _regex =
            new(@"Borrower\s*:\s*(.+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public IEnumerable<LegalFact> Apply(
            Guid documentId,
            int pageNumber,
            string text)
        {
            var match = _regex.Match(text);

            if (!match.Success)
                yield break;

            var borrowerName = match.Groups[1].Value.Trim();

            yield return new LegalFact(
                FactType.BorrowerName,
                new EvidenceReference(documentId, pageNumber, 0.95f),
                borrowerName);
        }
    }
}
