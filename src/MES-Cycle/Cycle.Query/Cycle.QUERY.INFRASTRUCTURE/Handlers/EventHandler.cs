using Cycle.Common.Events;
using Cycle.QUERY.DOMAIN.Entities;
using Cycle.QUERY.DOMAIN.repository;
using Cycle.QUERY.DOMAIN.Services;

namespace Cycle.QUERY.INFRASTRUCTURE.Handler
{
    public class EventHandler : IEventHandler
    {
        private readonly ICycleRepository _cycleRepository;
        private readonly IMachineConfigRepository _machineConfigRepository;
        private readonly IWebNotificationService _notificationService;

        public EventHandler(ICycleRepository postRepository,
                            IWebNotificationService notificationService,
                             IMachineConfigRepository machineConfigRepository)
        {
            _cycleRepository = postRepository;
            _machineConfigRepository = machineConfigRepository;
            _notificationService = notificationService;
            
        }
        public async Task On(CycleCreatedEvent @event)
        {
            var cycle = new CycleEntity
            {
                CycleId = @event.Id,
                Parts_per_cycle = @event.parts_per_cycle,
                Finished = @event.finished,
                ProductionOrderId = @event.productionOrderId,
                MachineConfigId = @event.machineConfigId,
                CreatedAt = DateTime.Now
            };

            await _cycleRepository.CreateAsync(cycle);
            var stats = await _cycleRepository.GetTodayDashboardStatsAsync();
            await _notificationService.SendUpdateAsync( "ReceiveCycleUpdate", stats);
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
                MachineIdSeq = @event.machineIdSeq,
                TimeConfig = DateTime.UtcNow
            };
            await _machineConfigRepository.CreateAsync(mConfig);
            var hourlyPressureData = await _machineConfigRepository.GetTodayHourlyPressureAsync();
            await _notificationService.SendUpdateAsync("ReceiveMachineUpdate", hourlyPressureData);
        }
    }
}