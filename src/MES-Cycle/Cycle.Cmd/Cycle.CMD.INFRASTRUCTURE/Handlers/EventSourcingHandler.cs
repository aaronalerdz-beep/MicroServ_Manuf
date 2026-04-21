using System.Runtime.InteropServices;
using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using MES_Cycle.CMD.DOMAIN.Aggregates;

namespace MES_Cycle.CMD.INFRASTRUCTURE.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<CycleAggregate>
    {
        private readonly IEventStore _eventStore;

        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore =eventStore;
        }
        public async Task<CycleAggregate> GetByIdAsync(Guid id)
        {
            var aggrgate = new CycleAggregate();
            var events = await _eventStore.GetEventsAsync(id);
            if(events == null || !events.Any()) return aggrgate;

            aggrgate.ReplayEvents(events);
            var latesVersion = events.Max(x => x.Version);

            return aggrgate;
        }

        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
            aggregate.MarkChangesAsCommitted();
        }
    }
}