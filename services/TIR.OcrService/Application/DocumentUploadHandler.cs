using Amazon.S3;
using Amazon.S3.Model;
using TIR.OcrService.Domain;
using TIR.OcrService.Infrastructure;
using TIR.SharedKernel.Audit;
using TIR.SharedKernel.Events;

namespace TIR.OcrService.Application
{
    public sealed class DocumentUploadHandler
    {
        private readonly IAmazonS3 _s3;
        private readonly OCREngine _ocrEngine;
        private readonly EventPublisher _publisher;
        private readonly IAuditPublisher _audit;
        private readonly IConfiguration _config;

        public DocumentUploadHandler(
            IAmazonS3 s3,
            OCREngine ocrEngine,
            EventPublisher publisher,
            IAuditPublisher audit,
            IConfiguration config)
        {
            _s3 = s3;
            _ocrEngine = ocrEngine;
            _publisher = publisher;
            _audit = audit;
            _config = config;
        }

        public async Task HandleAsync(
            DocumentUploadedEvent evt,
            CancellationToken ct)
        {
            var localPath = $"/tmp/{evt.DocumentId}";

            try
            {
               
                await _audit.PublishAsync(
                    new AuditEvent(
                        evt.TenantId,
                        "system",
                        "OCR started",
                        "Document",
                        evt.DocumentId.ToString(),
                        DateTime.UtcNow,
                        evt.CorrelationId),
                    ct);

                
                var bucket = _config["S3:DocumentBucket"];
                var key = $"tenant/{evt.TenantId}/project/{evt.ProjectId}/documents/{evt.DocumentId}/original";

                await DownloadAsync(bucket!, key, localPath, ct);

               
                var language = MapLanguage(evt.Language);

               
                var ocrDoc = _ocrEngine.RunOcr(
                    evt.DocumentId,
                    localPath,
                    language);

                var failedPages = ocrDoc.Pages
                    .Where(p => !p.IsReadable)
                    .Select(p => p.PageNumber)
                    .ToList();

                var avgConfidence = ocrDoc.Pages.Any()
                    ? ocrDoc.Pages.Average(p => p.Confidence)
                    : 0f;

               
                var completedEvent = new OcrCompletedEvent(
                    evt.TenantId,
                    evt.ProjectId,
                    evt.DocumentId,
                    ocrDoc.IsFullyReadable,
                    avgConfidence,
                    failedPages,
                    DateTime.UtcNow,
                    evt.CorrelationId);

                await _publisher.PublishAsync(completedEvent, ct);

              
                await _audit.PublishAsync(
                    new AuditEvent(
                        evt.TenantId,
                        "system",
                        ocrDoc.IsFullyReadable
                            ? "OCR completed successfully"
                            : "OCR completed with low confidence",
                        "Document",
                        evt.DocumentId.ToString(),
                        DateTime.UtcNow,
                        evt.CorrelationId),
                    ct);
            }
            catch (Exception ex)
            {
               
                await _audit.PublishAsync(
                    new AuditEvent(
                        evt.TenantId,
                        "system",
                        "OCR failed",
                        "Document",
                        evt.DocumentId.ToString(),
                        DateTime.UtcNow,
                        evt.CorrelationId),
                    ct);

                throw; 
            }
            finally
            {
                
                if (File.Exists(localPath))
                {
                    File.Delete(localPath);
                }
            }
        }

        private async Task DownloadAsync(
            string bucket,
            string key,
            string localPath,
            CancellationToken ct)
        {
            var response = await _s3.GetObjectAsync(
                new GetObjectRequest
                {
                    BucketName = bucket,
                    Key = key
                },
                ct);

            await using var fs = File.Create(localPath);
            await response.ResponseStream.CopyToAsync(fs, ct);
        }

        private static OCRLanguage MapLanguage(string? lang) =>
            lang?.ToUpperInvariant() switch
            {
                "EN" => OCRLanguage.EN,
                "AS" => OCRLanguage.AS,
                _ => OCRLanguage.UNKNOWN
            };
    }
}
