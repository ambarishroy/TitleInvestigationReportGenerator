using Microsoft.AspNetCore.Mvc;
using TIR.LegalExtractionService.Application;
using TIR.LegalExtractionService.Infrastructure;

namespace TIR.LegalExtractionService.Controllers;

[ApiController]
[Route("api/legal-extraction")]
public sealed class LegalExtractionController : ControllerBase
{
    private readonly FactExtractor _extractor = new();
    private readonly EventPublisher _publisher = new();

    [HttpPost("extract")]
    public IActionResult Extract([FromBody] ExtractLegalFactsRequest request)
    {
        var facts = _extractor.Extract(request);
        _publisher.PublishFactsExtracted(request.DocumentId, facts);

        return Ok(new ExtractLegalFactsResponse
        {
            DocumentId = request.DocumentId,
            Facts = facts
        });
    }
}
