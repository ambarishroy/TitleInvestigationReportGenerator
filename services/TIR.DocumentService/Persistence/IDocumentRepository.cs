

using TIR.DocumentService.Domain;

namespace TIR.DocumentService.Persistence
{
    public interface IDocumentRepository
    {
        Task AddAsync(Document document, string sha256Hash, CancellationToken ct);
        Task<Document?> GetByIdAsync(
           Guid documentId,
           CancellationToken ct);
    }
}
