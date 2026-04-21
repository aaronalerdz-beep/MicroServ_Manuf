using CQRS.Core.Events;

namespace Cycle.Common.Events;

public class MachineConfEvent : BaseEvent
{ 
    public MachineConfEvent() : base(nameof(MachineConfEvent)) { }

    public int pressure { get; set; }
    public int grit { get; set; }
    public int cycle_duration { get; set; }
    public string? operator_name { get; set; } 
    public int machineIdSeq { get; set; }
}