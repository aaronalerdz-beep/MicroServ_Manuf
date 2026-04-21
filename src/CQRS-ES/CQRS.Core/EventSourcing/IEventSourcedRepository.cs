using CQRS.Core.Domain;

namespace CQRS.Core.EventSourcing;

public interface IEventSourcedRepository<TAggregate> where TAggregate : AggregateRoot, new()
{
    Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
}

