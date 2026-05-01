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
        
        public DateTime CreatedAt {get; set;}

        // Comes from CycleCreatedEvent.machineConfigId (int).
        // This is an external identifier, not the FK to our local MachineConfig projection.
        public string? MachineConfigId { get; set; }
    }

    
}