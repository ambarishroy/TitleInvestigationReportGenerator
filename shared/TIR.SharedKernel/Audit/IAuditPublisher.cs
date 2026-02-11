using TIR.SharedKernel.Events;

namespace TIR.SharedKernel.Audit
{
    public interface IAuditPublisher
    {
        Task PublishAsync(AuditEvent auditEvent, CancellationToken ct);
    }
}
