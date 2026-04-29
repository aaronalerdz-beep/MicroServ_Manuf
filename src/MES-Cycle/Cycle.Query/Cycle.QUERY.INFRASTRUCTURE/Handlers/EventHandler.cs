using Cycle.Common.Events;
using Cycle.QUERY.DOMAIN.Entities;
using Cycle.QUERY.DOMAIN.repository;

namespace Cycle.QUERY.INFRASTRUCTURE.Handler
{
    public class EventHandler : IEventHandler
    {
        private readonly ICycleRepository _cycleRepository;
        private readonly IMachineConfigRepository _machineConfigRepository;

        public EventHandler(ICycleRepository postRepository, IMachineConfigRepository machineConfigRepository)
        {
            _cycleRepository = postRepository;
            _machineConfigRepository = machineConfigRepository;
            
        }
        public async Task On(CycleCreatedEvent @event)
        {
            var cycle = new CycleEntity
            {
                CycleId = @event.Id,
                Parts_per_cycle = @event.parts_per_cycle,
                Finished = @event.finished,
                ProductionOrderId = @event.productionOrderId,
                MachineConfigId = @event.machineConfigId
            };

            await _cycleRepository.CreateAsync(cycle);
        }

        public async Task On(MachineConfEvent @event)
        {
            var mConfig = new MachineConfigEntity
            {
                MachineConfigId = @event.Id,
                Pressure = @event.pressure,
                Grit = @event.grit,
                Cycle_duration = @event.cycle_duration,
                Operator_name = @event.operator_name,
                MachineIdSeq = @event.machineIdSeq

            };

            await _machineConfigRepository.CreateAsync(mConfig);
        }
    }
}