using CQRS.Core.Events;

namespace CQRS.Core.Domain
{ 
    public interface IEventStoreRepository
    {
        Task<List<EventModel>> FindByAggregateId(Guid aggregateId);
        Task SaveEventAsync(EventModel @event);
    }
}