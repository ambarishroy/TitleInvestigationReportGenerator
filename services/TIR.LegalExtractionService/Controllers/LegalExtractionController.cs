using Microsoft.AspNetCore.Mvc;
using TIR.LegalExtractionService.Application;
using TIR.LegalExtractionService.Extractors;
using TIR.LegalExtractionService.Infrastructure;
using TIR.SharedKernel.Events;

namespace TIR.LegalExtractionService.Controllers;

[ApiController]
[Route("api/legal-extraction")]
public sealed class LegalExtractionController : ControllerBase
{
    private readonly FactExtractor _extractor;
    private readonly EventPublisher _publisher;
    public LegalExtractionController(
        FactExtractor extractor,
        EventPublisher publisher)
    {
        _extractor = extractor;
        _publisher = publisher;
    }

    [HttpPost("extract")]
    public IActionResult Extract([FromBody] ExtractLegalFactsRequest request, OcrCompletedEvent evt, CancellationToken ct)
    {
        var facts = _extractor.Extract(request);
        _publisher.PublishFactsExtractedAsync( evt, facts,  ct);

        return Ok(new ExtractLegalFactsResponse
        {
            DocumentId = request.DocumentId,
            Facts = facts
        });
    }
}
