using Amazon.S3;
using Amazon.S3.Model;
using System.Text;
using TIR.LegalExtractionService.Infrastructure;
using TIR.SharedKernel.Events;

namespace TIR.LegalExtractionService.Application
{
    public sealed class OcrCompletedEventHandler
    {
        private readonly FactExtractor _extractor;
        private readonly EventPublisher _publisher;
        private readonly IAmazonS3 _s3;
        private readonly IConfiguration _config;

        public OcrCompletedEventHandler(
            FactExtractor extractor,
            EventPublisher publisher,
            IAmazonS3 s3,
            IConfiguration config)
        {
            _extractor = extractor;
            _publisher = publisher;
            _s3 = s3;
            _config = config;
        }

        public async Task HandleAsync(
            OcrCompletedEvent evt,
            CancellationToken ct)
        {
            if (!evt.IsFullyReadable)
                return;

            var bucket = _config["S3:DocumentBucket"];
            var key = $"tenant/{evt.TenantId}/project/{evt.ProjectId}/documents/{evt.DocumentId}/ocr/ocr.txt";

            
            var ocrText = await DownloadTextAsync(bucket!, key, ct);

            var pages = new Dictionary<int, string>
            {
                { 1, ocrText }
            };

            var request = new ExtractLegalFactsRequest
            {
                DocumentId = evt.DocumentId,
                Pages = pages
            };

            var facts = _extractor.Extract(request);

            
            await _publisher.PublishFactsExtractedAsync(
                evt,
                facts,
                ct);
        }

        private async Task<string> DownloadTextAsync(
            string bucket,
            string key,
            CancellationToken ct)
        {
            var response = await _s3.GetObjectAsync(
                new GetObjectRequest
                {
                    BucketName = bucket,
                    Key = key
                },
                ct);

            using var reader = new StreamReader(response.ResponseStream, Encoding.UTF8);
            return await reader.ReadToEndAsync(ct);
        }
    }
}
