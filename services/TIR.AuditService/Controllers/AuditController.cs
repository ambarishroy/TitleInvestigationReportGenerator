using Microsoft.AspNetCore.Mvc;
using TIR.AuditService.Application;
using TIR.AuditService.Domain;
using TIR.AuditService.Infrastructure;

namespace TIR.AuditService.Controllers
{
    [ApiController]
    [Route("api/audit")]
    public class AuditController : ControllerBase
    {
        private static readonly Dictionary<Guid, List<FinalTir>> _finalReports = new();
        private readonly FileStorageService _fileStorage = new();
        private readonly AuditPublisher _auditPublisher = new();

        [HttpPost("upload")]
        public IActionResult UploadFinalTir([FromBody] UploadFinalTirRequest request)
        {
            // Validate Project exists and user belongs to correct bank tenant (tenant isolation)
            // TODO: enforce using ProjectService repository + JWT claims
            if (!_finalReports.ContainsKey(request.ProjectId))
                _finalReports[request.ProjectId] = new List<FinalTir>();

            int version = _finalReports[request.ProjectId].Count + 1;

            string path = _fileStorage.SaveFinalTir("SBI", request.ProjectId, version, request.FileName, request.FileContent);

            var finalTir = new FinalTir(request.ProjectId, path, request.UploadedByUserId, version);
            finalTir.MarkReportGenerated();

            _finalReports[request.ProjectId].Add(finalTir);

            _auditPublisher.PublishFinalTirUploaded(finalTir);
            return Ok(new UploadFinalTirResponse
            {
                ProjectId = request.ProjectId,
                VersionNumber = version,
                Status = finalTir.Status
            });
        }

    }
}
