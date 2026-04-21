using CQRS.Core.Domain;
using Cycle.Common.Events;

namespace MES_Cycle.CMD.DOMAIN.Aggregates;

public sealed class CycleAggregate : AggregateRoot
{
    private bool _active;
    public bool Active { get => _active; private set => _active = value; }

    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

    public int PartsPerCycle { get; private set; }
    public int Finished { get; private set; }
    public int MachineConfigurationId { get; private set; }
    public int ProductionOrderId { get; private set; }

    public int Pressure { get; private set; }
    public int Grit { get; private set; }
    public int CycleDuration { get; private set; }
    // Inicializado para evitar el warning CS8618
    public string? OperatorName { get; private set; } 
    public int MachineIdSeq { get; private set; }

    public CycleAggregate()
    {
    }

    public CycleAggregate(Guid id, int partsPerCycle, int finished, int machineConfigurationId, int productionOrderId)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.", nameof(id));
                
        RaiseEvent(new CycleCreatedEvent
        {
            Id = id,
            parts_per_cycle = partsPerCycle,
            finished = finished,
            machineConfigurationId = machineConfigurationId,
            productionOrderId = productionOrderId
        });
    }

    public void ConfigureMachine(int pressure, int grit, int cycleDuration, string operatorName, int machineIdSeq)
    {

        RaiseEvent(new MachineConfEvent
        {
            Id = this.Id,
            pressure = pressure,
            grit = grit,
            cycle_duration = cycleDuration,
            operator_name = operatorName,
            machineIdSeq = machineIdSeq
        });
    }

    public void Apply(CycleCreatedEvent @event)
    {
        _id = @event.Id;
        PartsPerCycle = @event.parts_per_cycle;
        Finished = @event.finished;
        MachineConfigurationId = @event.machineConfigurationId;
        ProductionOrderId = @event.productionOrderId;
        _active = true;
    }

public void Apply(MachineConfEvent @event)
{
    Pressure = @event.pressure;
    Grit = @event.grit;
    CycleDuration = @event.cycle_duration;
    OperatorName = @event.operator_name;
    MachineIdSeq = @event.machineIdSeq;
    _active = true;
}
}