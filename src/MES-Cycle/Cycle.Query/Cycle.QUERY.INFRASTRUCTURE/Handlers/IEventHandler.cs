using Cycle.Common.Events;

namespace Cycle.QUERY.INFRASTRUCTURE.Handler
{
    public interface IEventHandler
    {
        Task On(CycleCreatedEvent @event);
        Task On(MachineConfEvent @event);
    }
}