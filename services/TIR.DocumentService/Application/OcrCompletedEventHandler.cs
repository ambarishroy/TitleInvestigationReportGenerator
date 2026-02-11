
using TIR.DocumentService.Persistence;
using TIR.SharedKernel.Audit;
using TIR.SharedKernel.Events;

namespace TIR.DocumentService.Application
{
    public sealed class OcrCompletedEventHandler
    {
        private readonly IDocumentRepository _repository;
        private readonly IAuditPublisher _audit;
        public OcrCompletedEventHandler(IDocumentRepository repository, IAuditPublisher audit)
        {
            _repository = repository;
            _audit = audit;
        }
        public async Task HandleAsync(OcrCompletedEvent evt, CancellationToken ct)
        {
            var document = await _repository.GetByIdAsync(evt.DocumentId, ct);

            if (document == null)
            {
                
                await _audit.PublishAsync(
                    new AuditEvent(
                        evt.TenantId,
                        "system",
                        "OCR completed but document not found",
                        "Document",
                        evt.DocumentId.ToString(),
                        DateTime.UtcNow,
                        evt.CorrelationId),
                    ct);

                return;
            }
            if (evt.IsFullyReadable)
            {
                document.MarkProcessed();
            }
            else
            {
                document.MarkActionRequired();
            }
            await _repository.UpdateAsync(document, ct);

            await _audit.PublishAsync(
               new AuditEvent(
                   evt.TenantId,
                   "system",
                   evt.IsFullyReadable
                       ? "Document OCR successful"
                       : "Document OCR requires action",
                   "Document",
                   document.DocumentId.ToString(),
                   DateTime.UtcNow,
                   evt.CorrelationId),
               ct);
        }
    }
}
