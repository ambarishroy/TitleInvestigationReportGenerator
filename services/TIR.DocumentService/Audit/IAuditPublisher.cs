using TIR.SharedKernel.Events;

namespace TIR.DocumentService.Audit
{
    public interface IAuditPublisher
    {
        Task PublishAsync(AuditEvent auditEvent, CancellationToken ct);
    }
}
