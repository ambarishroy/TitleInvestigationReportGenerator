using Amazon.S3;
using Amazon.S3.Model;

namespace TIR.DocumentService.Infrastructure.Storage
{
    public sealed class S3StorageService: IS3StorageService
    {
        private readonly IAmazonS3 _s3;
        public S3StorageService(IAmazonS3 s3) { _s3 = s3; }
        public async Task UploadAsync(string bucket, string key, Stream content, string contentType, CancellationToken ct)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucket,
                Key = key,
                InputStream = content,
                ContentType = contentType,
                ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
            };
            await _s3.PutObjectAsync(request, ct);
        }
    }
}
