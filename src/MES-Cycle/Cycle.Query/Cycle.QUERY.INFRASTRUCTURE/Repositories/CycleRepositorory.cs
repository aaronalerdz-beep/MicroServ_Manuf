using Cycle.QUERY.DOMAIN;
using Cycle.QUERY.DOMAIN.Entities;
using Cycle.QUERY.DOMAIN.repository;
using Cycle.QUERY.INFRASTRUCTURE.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Cycle.QUERY.INFRASTRUCTURE.Repositories
{
    public class CycleRepository: ICycleRepository
    {

        private readonly DatabaseContextFactory _contextFactory;

        public CycleRepository( DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task CreateAsync(CycleEntity cycle)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Cycles.Add(cycle);

            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid cycleid)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();

            var cycle = await GetByIdAsync(cycleid);

            if (cycle == null) return;

            context.Cycles.Remove(cycle);
            _ = await context.SaveChangesAsync();

        }

        public async Task<CycleEntity> GetByIdAsync(Guid cycleid)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Cycles.FirstOrDefaultAsync(x => x.CycleId == cycleid);
        }

        public async Task<List<CycleEntity>> ListAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Cycles.AsNoTracking().ToListAsync();
        }
    public async Task<DashboardStatsDto> GetTodayDashboardStatsAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        
        var today = DateTime.Today;

        var todayCycles = context.Cycles
            .AsNoTracking()
            .Where(c => c.CreatedAt >= today);

        return new DashboardStatsDto
        {
            TotalCyclesToday = await todayCycles.CountAsync(),
            TotalPartsToday = await todayCycles.SumAsync(c => c.Parts_per_cycle),
        };
    }
        

        public async Task UpdateAsync(CycleEntity cycle)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Cycles.Update(cycle);

            _ = await context.SaveChangesAsync();
        }
    }
}