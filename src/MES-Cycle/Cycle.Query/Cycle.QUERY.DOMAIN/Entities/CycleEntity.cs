using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cycle.QUERY.DOMAIN.Entities
{
    [Table("Cycle")]
    public class CycleEntity 
    {
        [Key]
        public Guid CycleId { get; set; }
        public int Parts_per_cycle { get; set; }
        public int Finished { get; set; }
        public int ProductionOrderId { get; set; }

        public Guid MachineConfigId { get; set; }

        [ForeignKey(nameof(MachineConfigId))]
        public virtual MachineConfigEntity MachineConfig { get; set; } = null!;
    }

    
}