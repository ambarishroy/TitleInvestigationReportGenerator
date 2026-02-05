using Amazon.S3;
using TIR.OcrService.Domain;
using TIR.OcrService.Infrastructure;
using TIR.SharedKernel.Events;

namespace TIR.OcrService.Application
{
    public sealed class DocumentUploadHandler
    {
        private readonly IAmazonS3 _s3;
        private readonly OCREngine _ocrEngine;
        private readonly EventPublisher _publisher;
        public DocumentUploadHandler(IAmazonS3 s3, OCREngine ocrEngine, EventPublisher publisher)
        {
            _s3 = s3;
            _ocrEngine = ocrEngine;
            _publisher = publisher;
        }
        public async Task HandleAsync(DocumentUploadedEvent evt, CancellationToken ct)
        {
            var localPath = $"/tmp/{evt.DocumentId}";

            await DownloadAsync(evt.S3Bucket, evt.S3Key, localPath, ct);

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
            await _publisher.PublishAsync(
              new OcrCompletedEvent(
                  evt.TenantId,
                  evt.ProjectId,
                  evt.DocumentId,
                  ocrDoc.IsFullyReadable,
                  avgConfidence,
                  failedPages,
                  DateTime.UtcNow,
                  evt.CorrelationId),
              ct);
        }
        private async Task DownloadAsync(string bucket,string key,string localPath,CancellationToken ct)
        {
            var obj = await _s3.GetObjectAsync(bucket, key, ct);
            await using var fs = File.Create(localPath);
            await obj.ResponseStream.CopyToAsync(fs, ct);
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
