using Cycle.QUERY.DOMAIN;
using Cycle.QUERY.DOMAIN.Entities;
using Cycle.QUERY.DOMAIN.repository;
using Cycle.QUERY.INFRASTRUCTURE.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Cycle.QUERY.INFRASTRUCTURE.Repositories
{
    public class MachineConfigRepository : IMachineConfigRepository
    {

        private readonly DatabaseContextFactory _contextFactory;

        public MachineConfigRepository( DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task CreateAsync(MachineConfigEntity machineConfig)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.MachineConfigs.Add(machineConfig);

            _ = await context.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(Guid machineConfigId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var machineConfig = await GetByIdAsync(machineConfigId);

            if (machineConfig == null) return;

            context.MachineConfigs.Remove(machineConfig);
            _ = await context.SaveChangesAsync();
        }

        public async Task<MachineConfigEntity> GetByIdAsync(Guid machineConfigId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();

            return await context.MachineConfigs.FirstOrDefaultAsync(x => x.MachineConfigId == machineConfigId);
       
        }

        public Task<List<MachineConfigEntity>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(MachineConfigEntity machineConfig)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.MachineConfigs.Update(machineConfig);

            _ = await context.SaveChangesAsync();
        }
    }
}