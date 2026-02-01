using System.Collections.Generic;
using TIR.SharedKernel.ValueObjects;
namespace TIR.TitleAnalysisService.Infrastructure;

public sealed class RiskAnalyzer
{
    public List<RiskItem> Analyze(List<LegalFact> facts)
    {
        var risks = new List<RiskItem>();

        foreach (var f in facts)
        {
            if (f.Value == "UNKNOWN")
            {
                risks.Add(new RiskItem($"Missing or unreadable data for {f.Type}", f.Evidence));
            }
        }

        return risks;
    }
}
