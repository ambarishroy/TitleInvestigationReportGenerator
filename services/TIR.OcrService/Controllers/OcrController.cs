using Microsoft.AspNetCore.Mvc;
using TIR.OcrService.Application;
using TIR.OcrService.Domain;
using TIR.OcrService.Infrastructure;

namespace TIR.OcrService.Controllers
{
    [ApiController]
    [Route("api/ocr")]
    public class OcrController : ControllerBase
    {
        private readonly EventPublisher _eventPublisher;
        private readonly OCREngine _ocrEngine;
        [HttpPost("process")]
        public IActionResult ProcessDocument([FromBody] ProcessDocumentRequest request)
        {
            var language = request.LanguageHint == "AS" ? OCRLanguage.AS : OCRLanguage.EN;
            var doc = _ocrEngine.RunOcr(request.DocumentId, request.FilePath, language);

            _eventPublisher.PublishOcrCompleted(
                request.DocumentId,
                doc.IsFullyReadable,
                doc.Pages.FindAll(p => !p.IsReadable).ConvertAll(p => p.PageNumber)
            );

            return Ok(new ProcessDocumentResponse
            {
                DocumentId = request.DocumentId,
                IsFullyReadable = doc.IsFullyReadable,
                FailedPages = doc.Pages.FindAll(p => !p.IsReadable).ConvertAll(p => p.PageNumber)
            });
        }
    }
}
