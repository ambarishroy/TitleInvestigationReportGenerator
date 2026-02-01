using Microsoft.AspNetCore.Mvc;
using TIR.ReportDraftService.Application;
using TIR.ReportDraftService.Infrastructure;

namespace TIR.ReportDraftService.Controllers
{
    [ApiController]
    [Route("api/draft-tir")]
    public sealed class DraftTirController:ControllerBase
    {
        private readonly DocumentGenerator _generator = new();
        [HttpPost("generate")]
        public IActionResult GenerateDraft([FromBody] GenerateDraftRequest request)
        {
            var filePath = _generator.GenerateDraftTir(request.ProjectSummary);

            return Ok(new GenerateDraftResponse
            {
                ProjectId = request.ProjectSummary.ProjectName != null ? Guid.NewGuid() : Guid.Empty,
                FileName = Path.GetFileName(filePath),
                Status = "UnderReview"
            });
        }
    }
}
