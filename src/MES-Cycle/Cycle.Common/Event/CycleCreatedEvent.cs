using CQRS.Core.Events;

namespace Cycle.Common.Events;

public class CycleCreatedEvent : BaseEvent
{
  
    public CycleCreatedEvent() : base(nameof(CycleCreatedEvent)) { }

    public int parts_per_cycle { get; set; }
    public int finished { get; set; }
    public required string machineConfigId { get; set; }
    public int productionOrderId { get; set; }
}