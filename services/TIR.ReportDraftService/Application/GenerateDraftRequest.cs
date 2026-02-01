using TIR.ReportDraftService.Domain;

namespace TIR.ReportDraftService.Application
{
    public sealed class GenerateDraftRequest
    {
        public TirProjectSummary ProjectSummary { get; init; } = default!;
    }
}
