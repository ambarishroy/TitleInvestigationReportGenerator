using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TIR.SharedKernel.ValueObjects;
using TIR.SharedKernel.Enums;
using TIR.LegalExtractionService.Application;

namespace TIR.LegalExtractionService.Infrastructure
{
    public sealed class FactExtractor
    {
        public List<LegalFact> Extract(ExtractLegalFactsRequest request)
        {
            var facts = new List<LegalFact>();
            foreach (var page in request.Pages)
            {
                int pageNo = page.Key;
                string text = page.Value;

                // Example: Borrower Name extraction
                var borrowerMatch = Regex.Match(text, @"Borrower\s*:\s*(.+)");
                var borrowerName = borrowerMatch.Success ? borrowerMatch.Groups[1].Value.Trim() : "UNKNOWN";
                
                facts.Add(new LegalFact(FactType.BorrowerName,
                    new EvidenceReference(request.DocumentId, pageNo, 0.95f), borrowerName));

                // Example: Deed Number extraction
                var deedMatch = Regex.Match(text, @"Deed No\.\s*(\d+)");
                var deedNo = deedMatch.Success ? deedMatch.Groups[1].Value.Trim() : "UNKNOWN";
                facts.Add(new LegalFact(FactType.DeedNumber,
                    new EvidenceReference(request.DocumentId, pageNo, 0.95f), deedNo));

                // TODO: Add more deterministic rules per SBI TIR template
            }
            return facts;
        }
    }
}
