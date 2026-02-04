using TIR.DocumentService.Domain;
using Microsoft.EntityFrameworkCore;

namespace TIR.DocumentService.Persistence
{
    public sealed class DocumentRepository: IDocumentRepository
    {
        private readonly DocumentDbContext _db;
        public DocumentRepository(DocumentDbContext db) { _db = db; }
        public async Task AddAsync(Document document, string sha256Hash, CancellationToken ct)
        {
            var record = new DocumentRecord
            {
                DocumentId = document.DocumentId,
                ProjectId = document.ProjectId,
                FileName = document.FileName,
                PageCount = document.PageCount,
                Type = document.Type,
                Status = document.Status,
                Language = document.Language,
                UploadedBy = document.UploadedBy,
                UploadedAtUtc = document.UploadedAtUtc,
                Sha256Hash = sha256Hash
            };

            _db.Documents.Add(record);
            await _db.SaveChangesAsync(ct);
        }
        public async Task<Document?> GetByIdAsync(Guid documentId, CancellationToken ct)
        {
            var record = await _db.Documents
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DocumentId == documentId, ct);

            if (record == null) return null;

            return new Document(
                record.ProjectId,
                record.FileName,
                record.PageCount,
                record.Type,
                record.Language,
                record.UploadedBy);
        }
    }
}
