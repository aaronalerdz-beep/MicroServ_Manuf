using CQRS.Core.Events;
using CQRS.Core.Infrastructure;
using CQRS.Core.Domain;
using CQRS.Core.Exeptions;
using CQRS.Core.Producers;

namespace Cycle.CMD.INFRASTRUCTURE.Stores
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventProducer _eventProducer;

        public EventStore(IEventStoreRepository eventSoreRepository, IEventProducer eventProducer)
        {
            _eventStoreRepository = eventSoreRepository;
            _eventProducer = eventProducer;
        }

        public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
            if(eventStream == null || !eventStream.Any()){
                throw new AggregateNotFoundExeption("Incorrect post ID provided");
            }
            return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
        }

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

            if(expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            {
                throw new ConcurrencyExeption();
            }

            var version = expectedVersion;

            foreach(var @event in events)
            {
                version++;
                @event.Version = version;
                var eventType = @event.GetType().Name;
                var eventModel = new EventModel
                {
                    Timestamp = DateTime.Now,
                    AggregateIdentifier = aggregateId,
                    Version = version,
                    EventType = eventType,
                    EventData = @event
                };
                await _eventStoreRepository.SaveEventAsync(eventModel);
                
                var topic = @event.GetType().Name; 
                await _eventProducer.ProducerAsync(topic, @event);
            }

        }
    }
}