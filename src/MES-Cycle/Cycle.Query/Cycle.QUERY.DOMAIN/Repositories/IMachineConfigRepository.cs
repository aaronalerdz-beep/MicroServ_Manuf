using Cycle.QUERY.DOMAIN.Entities;

namespace Cycle.QUERY.DOMAIN.repository
{
    public interface IMachineConfigRepository
    {
        Task CreateAsync(MachineConfigEntity machineConfig);
        Task UpdateAsync(MachineConfigEntity machineConfig);

        Task DeleteAsync(Guid machineConfigId);
        Task<MachineConfigEntity> GetByIdAsync(Guid machineConfigId);
        Task<List<MachineConfigEntity>> ListAllAsync();
    }
}