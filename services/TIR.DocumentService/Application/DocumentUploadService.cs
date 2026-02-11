using TIR.DocumentService.Common;
using TIR.DocumentService.Domain;
using TIR.DocumentService.Events;
using TIR.DocumentService.Infrastructure.Storage;
using TIR.DocumentService.Persistence;
using TIR.SharedKernel.Audit;
using TIR.SharedKernel.Events;

namespace TIR.DocumentService.Application
{
    public sealed class DocumentUploadService
    {
        private readonly IDocumentRepository _repository;
        private readonly IS3StorageService _storage;
        private readonly IDomainEventPublisher _events;
        private readonly IAuditPublisher _audit;
        private readonly IConfiguration _config;

        public DocumentUploadService(IDocumentRepository repository,
        IS3StorageService storage,
        IDomainEventPublisher events,
        IAuditPublisher audit,
        IConfiguration config)
        {
            _repository = repository;
            _storage = storage;
            _events = events;
            _audit = audit;
            _config = config;
        }
        public async Task<UploadDocumentResponse> UploadAsync(Guid tenantId, UploadDocumentRequest request, Stream fileStream, string contentType, string uploadedBy, string correlationId, CancellationToken ct)
        {
            var hash = DocumentHashService.ComputeHash(fileStream);
            fileStream.Position = 0;

            var document = new Document(request.ProjectId, request.FileName, request.PageCount, request.Type, request.Language, uploadedBy);

            await _repository.AddAsync(document, hash, ct);

            var bucket = _config["S3:DocumentBucket"];
            var key = DocumentKeyBuilder.Build(
                tenantId,
                document.ProjectId,
                document.DocumentId,
                Path.GetExtension(request.FileName));

            await _storage.UploadAsync(bucket, key, fileStream, contentType, ct);
           
            await _events.PublishAsync(
           new DocumentUploadedEvent(
               tenantId,
               document.ProjectId,
               document.DocumentId,
               document.Type,
               document.FileName,
               document.PageCount,
               document.Language,
               bucket,
               key,
               document.UploadedAtUtc,
               correlationId),
           correlationId,
           ct);
            await _audit.PublishAsync(
          new AuditEvent(
              tenantId,
              uploadedBy,
              "Document uploaded",
              "Document",
              document.DocumentId.ToString(),
              DateTime.UtcNow,
              correlationId),
          ct);
            return new UploadDocumentResponse
            {
                DocumentId = document.DocumentId,
                Status = document.Status.ToString()
            };
        }
    }
}
