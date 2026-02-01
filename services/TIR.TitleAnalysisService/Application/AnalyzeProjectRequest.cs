using System;
using System.Collections.Generic;
//using TIR.LegalExtractionService.Domain;
using TIR.SharedKernel.ValueObjects;
namespace TIR.TitleAnalysisService.Application;

public sealed class AnalyzeProjectRequest
{
    public Guid ProjectId { get; init; }
    public List<LegalFact> Facts { get; init; } = new();
}
