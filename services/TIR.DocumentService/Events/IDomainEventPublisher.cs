namespace TIR.DocumentService.Events
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync<T>(T domainEvent, string correlationId, CancellationToken ct);
    }
}
