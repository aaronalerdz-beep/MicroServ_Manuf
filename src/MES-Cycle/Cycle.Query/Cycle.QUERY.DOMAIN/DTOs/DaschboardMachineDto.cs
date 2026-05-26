
using Cycle.QUERY.DOMAIN.Entities;

public class DaschboardMachineDto
{
    public DateTime TimeConfig { get; set; }
    public int Pressure { get; set; }
    public int Grit { get; set; }
    public int Cycle_duration { get; set; }
    public string? Operator_name { get; set; }
}