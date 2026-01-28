using Microsoft.AspNetCore.Mvc;
using TIR.ProjectService.Application;
using TIR.ProjectService.Domain;

namespace TIR.ProjectService.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public sealed class ProjectsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateProject([FromBody] CreateProjectRequest request)
        {
            var createdByUserId = "TEMP-USER";
            var bankTenantId = "SBI";
            var jurisdictionCode = "IN-AS";

            if (string.IsNullOrWhiteSpace(request.ProjectName))
            {
                return BadRequest("ProjectName is required.");
            }
            var project = new TIRProject(request.ProjectName, bankTenantId, jurisdictionCode, request.LoanType, createdByUserId);
            // TODO: Persist using repository
            // TODO: Publish ProjectCreated event
            return Ok(new CreateProjectResponse { ProjectId=project.ProjectId, Status=project.Status.ToString()});
        }
    }
}
