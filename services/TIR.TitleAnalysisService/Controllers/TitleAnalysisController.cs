using Microsoft.AspNetCore.Mvc;
using TIR.TitleAnalysisService.Application;
using TIR.TitleAnalysisService.Infrastructure;

namespace TIR.TitleAnalysisService.Controllers;

[ApiController]
[Route("api/title-analysis")]
public sealed class TitleAnalysisController : ControllerBase
{
    private readonly TimelineBuilder _timelineBuilder = new();
    private readonly RiskAnalyzer _riskAnalyzer = new();

    [HttpPost("analyze")]
    public IActionResult AnalyzeProject([FromBody] AnalyzeProjectRequest request)
    {
        var timeline = _timelineBuilder.BuildTimeline(request.ProjectId, request.Facts);
        var risks = _riskAnalyzer.Analyze(request.Facts);

        return Ok(new AnalyzeProjectResponse
        {
            ProjectId = request.ProjectId,
            Timeline = timeline,
            Risks = risks
        });
    }
}
