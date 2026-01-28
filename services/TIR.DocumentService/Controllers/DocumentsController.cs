using Microsoft.AspNetCore.Mvc;
using TIR.DocumentService.Application;
using TIR.DocumentService.Domain;

namespace TIR.DocumentService.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public sealed class DocumentsController : ControllerBase
    {
        private static readonly List<Document> _documents = new(); // In-memory for now
        [HttpPost]
        public IActionResult UploadDocument([FromBody] UploadDocumentRequest request)
        {
            // TODO: validate Project exists & in Draft state
            // and check Project check
            var document = new Document(request.ProjectId, request.FileName, request.PageCount, request.Type, request.Language, request.UploadedBy);
            _documents.Add(document);
            // TODO: persist to DB
            // TODO: publish DocumentUploaded event
            return Ok(new UploadDocumentResponse { DocumentId = document.DocumentId, Status = document.Status.ToString() });
        }
    }
}
