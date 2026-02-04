namespace TIR.DocumentService.Infrastructure.Storage
{
    public interface IS3StorageService
    {
        Task UploadAsync(string bucket, string key, Stream content, string contentType, CancellationToken ct);
    }
}
