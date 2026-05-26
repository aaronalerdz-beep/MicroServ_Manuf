using Microsoft.EntityFrameworkCore;
using Cycle.QUERY.DOMAIN;
using Cycle.QUERY.DOMAIN.Entities;

namespace Cycle.QUERY.INFRASTRUCTURE.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CycleEntity> Cycles { get; set; }
        public DbSet<MachineConfigEntity> MachineConfigs { get; set; }

    }
}