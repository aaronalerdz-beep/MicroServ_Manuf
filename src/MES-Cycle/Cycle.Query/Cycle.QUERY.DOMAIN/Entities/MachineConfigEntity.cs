
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cycle.QUERY.DOMAIN.Entities
{
[Table("MachineConfig")]
public class MachineConfigEntity
{
    [Key]
    public Guid MachineConfigId { get; set; }
    public int Pressure { get; set; }
    public int Grit { get; set; }
    public int Cycle_duration { get; set; }
    public string? Operator_name { get; set; }
    public int MachineIdSeq { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<CycleEntity> Cycles { get; set; } = new HashSet<CycleEntity>();
}
        
}