using System.Collections.Generic;
using TIR.SharedKernel.ValueObjects;

namespace TIR.TitleAnalysisService.Application;

public sealed class AnalyzeProjectResponse
{
    public Guid ProjectId { get; init; }
    public OwnershipTimeline Timeline { get; init; } = new();
    public List<RiskItem> Risks { get; init; } = new();
    public bool HasGaps => Timeline.HasGaps();
}
