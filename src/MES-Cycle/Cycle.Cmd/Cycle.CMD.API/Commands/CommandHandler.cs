    using CQRS.Core.Commands;
    using CQRS.Core.Handlers;
    using MES_Cycle.CMD.DOMAIN.Aggregates;

    namespace Cycle.Cmd.Commands;

    public class CommandHandler : ICommandHandler
    {
        private readonly IEventSourcingHandler<CycleAggregate> _eventSourcingHandler;

        public CommandHandler(IEventSourcingHandler<CycleAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }
        public async Task HandlesAsync(NewCycleCommand command)
        {
            var aggregate = new CycleAggregate(command.Id, command.parts_per_cycle, command.finished, command.machineConfigId, command.productionOrderId);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }
public async Task HandlesAsync(MachineConfigCommand command)
{
    var aggregate = new CycleAggregate(); 
    
    
    // IMPORTANTE: Primero el ID
    aggregate.Id = command.Id; 

    aggregate.ConfigureMachine(
        command.pressure, 
        command.grit, 
        command.cycle_duration, 
        command.operator_name, 
        command.machineIdSeq
    );

    // 4. Guardas el nuevo agregado con su primer evento en MongoDB
    await _eventSourcingHandler.SaveAsync(aggregate);
}
    }