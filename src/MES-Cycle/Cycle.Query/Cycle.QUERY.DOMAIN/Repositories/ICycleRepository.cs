using Cycle.QUERY.DOMAIN.Entities;

namespace Cycle.QUERY.DOMAIN.repository
{
    public interface ICycleRepository
    {
        Task CreateAsync(CycleEntity cycle);
        Task UpdateAsync(CycleEntity cycle);

        Task<List<DashboardStatsDto>> GetTodayDashboardStatsAsync();
        Task DeleteAsync(Guid cycleid);
        Task<CycleEntity> GetByIdAsync(Guid cycleid);
        Task<List<CycleEntity>> ListAllAsync();
    }
}